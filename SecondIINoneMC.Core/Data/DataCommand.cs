using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Multiview.Data
{
    /// <summary>
    /// Represents a command wrapper that helps prevent a lot repetitive code.
    /// </summary>
    /// <remarks>
    /// This class prevents client classes from having to understand all
    /// the exceptions that can be thrown by the Data Provider.
    /// </remarks>
    public class DataCommand : Object, IDisposable
    {

        #region Private Fields

        private DataConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private bool _rollbackOnError;

        #endregion

        #region Constructor / Initialize

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        public DataCommand()
            : this(null, CommandType.Text, String.Empty, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCommand"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public DataCommand(DataConnection connection)
            : this(connection, CommandType.Text, String.Empty, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCommand" /> class for 
        /// a text based command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        public DataCommand(string commandText)
            : this(null, CommandType.Text, commandText, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCommand" /> class for 
        /// a text based command.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="commandText">The command text.</param>
        public DataCommand(DataConnection connection, string commandText)
            : this(connection, CommandType.Text, commandText, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCommand" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="type">The type of the command.</param>
        /// <param name="commandText">The command text.</param>
        public DataCommand(DataConnection connection, CommandType type, string commandText)
            : this(connection, type, commandText, false)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DataCommand" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="type">The type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="rollbackOnError">if set to <c>true</c> [rollback on error].</param>
        public DataCommand(DataConnection connection, CommandType type, string commandText, bool rollbackOnError)
        {
            _sqlCommand = new SqlCommand();

            // Init locals
            _sqlCommand.CommandText = commandText;
            _sqlCommand.CommandType = (CommandType)type;
            _sqlConnection = connection;
            _rollbackOnError = rollbackOnError;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DataCommand"/> class.
        /// </summary>
        ~DataCommand()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        /// <exception cref="System.Exception">The action failed.</exception>
        public void Dispose(bool managed)
        {
            if (managed)
            {
                try
                {
                    if (_sqlConnection != null)
                    {
                        _sqlConnection = null;
                    }

                    if (_sqlCommand != null)
                    {
                        _sqlCommand.Dispose();
                        _sqlCommand = null;
                    }
                }
                catch (Exception)
                {
                    // Do not throw
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the SQL statement or stored procedure to 
        /// execute on the data source.
        /// </summary>
        /// <value>
        /// <para>The default value is empty string.</para>
        /// </value>
        /// <remarks>
        /// <para>The SQL statement or stored procedure to execute at the data source.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">thrown when the passed value is invalid.</exception>
        public string CommandText
        {
            get
            {
                return _sqlCommand.CommandText;
            }
            set
            {
                ArgumentValidator.ThrowOnNullOrEmpty("value", value);

                _sqlCommand.CommandText = value;
            }
        }

        /// <summary>
        /// Gets or sets the time in seconds to wait for the command to execute.
        /// </summary>
        /// <value>
        /// <para>
        /// The time in seconds to wait for the command to execute. 
        /// The default is 30 seconds.
        /// </para>
        /// </value>
        public int CommandTimeout
        {
            get
            {
                return _sqlCommand.CommandTimeout;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _sqlCommand.CommandTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>
        /// The type of the command.
        /// </value>
        public CommandType CommandType
        {
            get
            {
                return _sqlCommand.CommandType;
            }
            set
            {
                _sqlCommand.CommandType = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBConn object used by this instance of the SqlCommand.
        /// </summary>
        /// <value>
        /// <para>The default value is null</para>
        /// </value>
        /// <remarks>
        /// <para>The DBConn instance used by this instance of the SqlCommand.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">thrown when the passed value is invalid.</exception>
        public DataConnection Connection
        {
            get
            {
                return _sqlConnection;
            }
            set
            {
                ArgumentValidator.ThrowOnNull("value", value);
                ArgumentValidator.ThrowOnCondition(value.IsAvailable == false, "Connection passed isn't in a state that it can be used.");

                _sqlConnection = value;

                _sqlCommand.Connection = _sqlConnection.InnerConnection;
                _sqlCommand.Transaction = _sqlConnection.InnerTransaction;
            }
        }

        #endregion

        #region Private Methods

        private void ThrowIfInvalid()
        {
            ArgumentValidator.ThrowOnNullOrEmpty("CommandText", _sqlCommand.CommandText);
            ArgumentValidator.ThrowOnNull("Connection", Connection);
            ArgumentValidator.ThrowOnCondition(!Connection.IsAvailable, "Connection passed is closed.");

            _sqlCommand.Connection = _sqlConnection.InnerConnection;
            _sqlCommand.Transaction = _sqlConnection.InnerTransaction;
        }

        #endregion

        #region AddParam Methods

        /// <summary>
        /// Adds an input parameter where the value assignment is done later.
        /// </summary>
        /// <param name="parameterName">Parameter name used by command.</param>
        /// <param name="typeIn">Parameters type used to format the data.</param>
        /// <exception cref="ArgumentException">
        /// thrown when <paramref name="parameterName"/> is <c>null</c> or empty.
        /// </exception>
        public DataCommandParameter AddParam(string parameterName, DataElementType typeIn)
        {
            return AddParam(parameterName, typeIn, null, ParameterDirection.Input);
        }

        /// <summary>
        /// Adds a parameter where the value assignment is done later.
        /// </summary>
        /// <param name="parameterName">Parameter name used by command.</param>
        /// <param name="typeIn">Parameters type used to format the data.</param>
        /// <param name="direction">Parameters direction. In/Out/both</param>
        /// <exception cref="ArgumentException">
        /// thrown when <paramref name="parameterName"/> is <c>null</c> or empty.
        /// </exception>
        public DataCommandParameter AddParam(string parameterName, DataElementType typeIn, ParameterDirection direction)
        {
            return AddParam(parameterName, typeIn, null, direction);
        }

        /// <summary>
        /// Adds an input parameter with the value assignment.
        /// </summary>
        /// <param name="parameterName">Parameter name used by command.</param>
        /// <param name="typeIn">Parameters type used to format the data.</param>
        /// <param name="value">object to be set.</param>
        /// <exception cref="ArgumentException">
        /// thrown when <paramref name="parameterName"/> is <c>null</c> or empty.
        /// </exception>
        public DataCommandParameter AddParam(string parameterName, DataElementType typeIn, object value)
        {
            return AddParam(parameterName, typeIn, value, ParameterDirection.Input);
        }

        /// <summary>
        /// Adds a parameter with all options.
        /// </summary>
        /// <param name="parameterName">Parameter name used by command.</param>
        /// <param name="typeIn">Parameters type used to format the data.</param>
        /// <param name="value">object to be set.</param>
        /// <param name="direction">Parameters direction. In/Out/both</param>
        /// <exception cref="ArgumentException">
        /// thrown when <paramref name="parameterName"/> is <c>null</c> or empty.
        /// </exception>
        /// <remarks>
        /// <para>
        /// By using this method no other properties have to be set to execute the command.
        /// </para>
        /// </remarks>
        public DataCommandParameter AddParam(string parameterName, DataElementType typeIn, object value, ParameterDirection direction)
        {
            ArgumentValidator.ThrowOnNullOrEmpty("parameterName", parameterName);

            var sqlParam = new SqlParameter(parameterName, (SqlDbType)typeIn);

            _sqlCommand.Parameters.Add(sqlParam);

            var p = new DataCommandParameter(sqlParam)
                .Value(value, false)
                .Direction(direction);

            return p;
        }

        #endregion

        #region SetParam Methods
        
        /// <summary>
        /// Updates a parameter already added to the class
        /// </summary>
        /// <param name="paramIndex">Parameters index to update</param>
        /// <param name="valueIn">Object to assign value to. If null DBNull will be assigned.</param>
        /// <param name="valueInIsNull">If DBNull should be assigned.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// thrown when <paramref name="paramIndex"/> is out of range of parameters added.
        /// </exception>
        /// <remarks>
        /// This method updates an existing parameter previously added.
        /// </remarks>
        public void SetParam(int paramIndex, object valueIn, bool valueInIsNull)
        {
            ArgumentValidator.ThrowOnOutOfRange("paramIndex", paramIndex, 0, _sqlCommand.Parameters.Count - 1, 
                                                "out of range of paramters added");

            new DataCommandParameter(_sqlCommand.Parameters[paramIndex])
                .Value(valueIn, valueInIsNull);
        }

        #endregion

        #region GetParam Methods

        /// <summary>
        /// Gets a parameters output value or <c>null</c>.
        /// </summary>
        /// <param name="paramIndex">Parameters index to get.</param>
        /// <returns>
        /// The object returned by the procedure which may be <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Index out of range of the parameters already added.
        /// </exception>
        /// <remarks>
        /// This method returns the parameters object, previously added by the AddParam method. 
        /// </remarks>
        public object GetParam(int paramIndex)
        {
            ArgumentValidator.ThrowOnOutOfRange("paramIndex", paramIndex, 0, _sqlCommand.Parameters.Count - 1,
                                                "out of range of paramters added");

            SqlParameter sqlParam = _sqlCommand.Parameters[paramIndex];

            return sqlParam.Value == DBNull.Value ? null : sqlParam.Value;
        }

        /// <summary>
        /// Gets if the parameters output value was null.
        /// </summary>
        /// <param name="paramIndex">Parameters index to get.</param>
        /// <returns>
        /// <para>Is the output object from the parameter null?</para>
        /// </returns>
        /// <exception cref="ArgumentException">Parameter being asked for was not set up with a direction of output.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Index out of range of the parameters already added.</exception>
        /// <remarks>
        /// This method returns if the parameters object returned is null. 
        /// </remarks>
        public bool GetParamIsNull(int paramIndex)
        {
            return GetParam(paramIndex) == DBNull.Value;
        }

        #endregion

        #region Clear Parameter Method

        /// <summary>
        /// Clears the current parameter collection held by the command object
        /// </summary>
        public void ClearParameters()
        {
            _sqlCommand.Parameters.Clear();
        }

        #endregion

        #region Execute Methods

        /// <summary>
        /// Asks the database to prepare the statement for multipule reuse
        /// </summary>
        /// <exception cref="DataCommandException">
        /// If any of the following are true: 
        /// CommandText is null --or--
        /// Connection is null  --or--
        /// An exception occurred while executing the command. --or--
        /// The connection does not exist or is not open.
        /// </exception>
        public void Prepare()
        {
            ThrowIfInvalid();

            try
            {
                _sqlCommand.Prepare();
            }
            catch (Exception ex)
            {
                throw new DataCommandException(ErrorType.CommandExecuteError,
                    ex.Message, ex);
            }
        }

        /// <summary>
        /// Executes the command text and returns a single row result as an object array.
        /// </summary>
        /// <returns>null if nothing is returned or an array of column values.</returns>
        /// <exception cref="DataCommandException">
        /// If any of the following are true: 
        /// CommandText is null --or--
        /// Connection is null  --or--
        /// An exception occurred while executing the command --or--
        /// The connection does not exist or is not open --or--
        /// Command Failed to execute.
        /// </exception>
        /// <remarks>
        /// <para>When set, if an exception is raised, the connections transaction will be rolled back.</para>
        /// </remarks>
        public DataCommandResultRow ExecuteGetSingleRow()
        {
            DataCommandResultRow row = null;

            ThrowIfInvalid();

            try
            {
                using (SqlDataReader sqlReader = _sqlCommand.ExecuteReader(CommandBehavior.Default))
                {
                    object[] values;

                    if (sqlReader != null)
                    {
                        if (sqlReader.HasRows)
                        {
                            while (sqlReader.Read())
                            {
                                values = new object[sqlReader.FieldCount];

                                sqlReader.GetValues(values);

                                row = new DataCommandResultRow(values);

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (_rollbackOnError)
                {
                    Connection.Rollback();
                }

                throw new DataCommandException(ErrorType.CommandGetError, "Data query execute failed.", ex);
            }

            return row;
        }

        /// <summary>
        /// Executes the command text and returns a list of object arrays.
        /// </summary>
        /// <exception cref="DataCommandException">
        /// If any of the following are true: 
        /// CommandText is null --or--
        /// Connection is null  --or--
        /// An exception occurred while executing the command --or--
        /// The connection does not exist or is not open --or--
        /// Command failed to execute.
        /// </exception>
        /// <returns>A list of array object. If no records are returned the object will be returned, but its count property will be zero.</returns>
        /// <remarks>
        /// <para>When set, if an exception is raised, the connections transaction will be rolled back.</para>
        /// </remarks>
        public DataCommandResultList ExecuteGetSingleResult()
        {
            DataCommandResultList list = new DataCommandResultList();

            ThrowIfInvalid();

            try
            {
                using (SqlDataReader sqlReader = _sqlCommand.ExecuteReader(CommandBehavior.Default))
                {
                    object[] values;

                    if (sqlReader != null)
                    {
                        if (sqlReader.HasRows)
                        {
                            while (sqlReader.Read())
                            {
                                values = new object[sqlReader.FieldCount];

                                sqlReader.GetValues(values);

                                list.Add(new DataCommandResultRow(values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (_rollbackOnError)
                {
                    Connection.Rollback();
                }

                throw new DataCommandException(ErrorType.CommandGetError, "Data query execute failed.", ex);
            }

            return list;
        }

        /// <summary>
        /// Executes the command text and returns a list of list of object arrays.
        /// </summary>
        /// <exception cref="DataCommandException">
        /// If any of the following are true: 
        /// CommandText is null --or--
        /// Connection is null  --or--
        /// An exception occurred while executing the command --or--
        /// The connection does not exist or is not open --or--
        /// Command failed to execute.
        /// </exception>
        /// <returns>A list of array object. If no records are returned the object will be returned, but its count property will be zero.</returns>
        /// <remarks>
        /// <para>When set, if an exception is raised, the connections transaction will be rolled back.</para>
        /// </remarks>
        public List<DataCommandResultList> ExecuteGetMultiResult()
        {
            List<DataCommandResultList> results = new List<DataCommandResultList>();

            ThrowIfInvalid();

            try
            {
                using (SqlDataReader sqlReader = _sqlCommand.ExecuteReader(CommandBehavior.Default))
                {
                    DataCommandResultList list;
                    object[] values;

                    if (sqlReader != null)
                    {
                        do
                        {
                            list = new DataCommandResultList();

                            if (sqlReader.HasRows)
                            {
                                while (sqlReader.Read())
                                {
                                    values = new object[sqlReader.FieldCount];

                                    sqlReader.GetValues(values);

                                    list.Add(new DataCommandResultRow(values));
                                }
                            }

                            results.Add(list);
                        } 
                        while (sqlReader.NextResult());
                    }
                }
            }
            catch (Exception ex)
            {
                if (_rollbackOnError)
                {
                    Connection.Rollback();
                }

                throw new DataCommandException(ErrorType.CommandGetError, "Data query execute failed.", ex);
            }

            return results;
        }

        /// <summary>
        /// Executes a query that does not return data using the Command 
        /// information that was set.
        /// </summary>
        /// <exception cref="DataCommandException">
        /// If any of the following are true: 
        /// CommandText is null --or--
        /// Connection is null  --or--
        /// An exception occurred while executing the command --or--
        /// The connection does not exist or is not open --or--
        /// Command failed to execute.
        /// </exception>
        /// <returns>Number of rows affected.</returns>
        /// <remarks>
        /// Simply executes the non-dataset returning query.
        /// </remarks>
        public int ExecuteNonQuery()
        {
            int intAffectedOut = 0;

            ThrowIfInvalid();

            try
            {
                intAffectedOut = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (_rollbackOnError)
                {
                    Connection.Rollback();
                }

                throw new DataCommandException(ErrorType.CommandExecuteError, "Execute query execute failed.", ex);
            }

            return intAffectedOut;
        }

        /// <summary>
        /// Executes a query that returns a single object
        /// information that was set.
        /// </summary>
        /// <exception cref="DataCommandException">
        /// If any of the following are true: 
        /// CommandText is null --or--
        /// Connection is null  --or--
        /// An exception occurred while executing the command --or--
        /// The connection does not exist or is not open --or--
        /// Command failed to execute.
        /// </exception>
        /// <returns>The resulting object</returns>
        /// <remarks>
        /// Simply executes the query and returns the result as an object.
        /// </remarks>
        public Object ExecuteScalar()
        {
            Object result = null;

            ThrowIfInvalid();

            try
            {
                result = _sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (_rollbackOnError)
                {
                    Connection.Rollback();
                }

                throw new DataCommandException(ErrorType.CommandExecuteScalarError, "Scalar query execute failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the last identity value used during the last insert.
        /// </summary>
        /// <exception cref="DataCommandException">
        /// thrown when the @@Identity returned null.
        /// </exception>
        /// <returns>
        /// This method returns the last identity value used during the last insert.
        /// </returns>
        public int GetLastIdentity()
        {
            int identityOut = -1;

            try
            {
                using (var sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "Select @@Identity";
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Connection = _sqlCommand.Connection;
                    sqlCommand.Transaction = _sqlCommand.Transaction;

                    using (var sqlReader = sqlCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        if (sqlReader != null)
                        {
                            while (sqlReader.Read())
                            {
                                var colValue = sqlReader.GetValue(0);

                                if (colValue == DBNull.Value)
                                {
                                    throw new Exception("Could not get last identity value inserted.");
                                }
                                else
                                {
                                    identityOut = Convert.ToInt32(colValue);
                                }

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (_rollbackOnError)
                {
                    Connection.Rollback();
                }

                throw new DataCommandException(ErrorType.CommandGetLastIdentity, "Get last identity failed.", ex);
            }

            return identityOut;
        }

        #endregion

        #region Inner Classes

        /// <summary>
        /// A command parameter
        /// </summary>
        public class DataCommandParameter
        {
            SqlParameter _param;

            /// <summary>
            /// Initializes a new instance of the <see cref="DataCommandParameter"/> class.
            /// </summary>
            /// <param name="param">The param.</param>
            /// <exception cref="ArgumentNullException">
            /// </exception>
            public DataCommandParameter(SqlParameter param)
            {
                ArgumentValidator.ThrowOnNull("param", param);

                _param = param;
            }

            /// <summary>
            /// Sets the parameter directions which has a default of <see cref="ParameterDirection.Input"/>
            /// </summary>
            /// <param name="direction">The direction to assign.</param>
            /// <returns>A fluent version of this instance.</returns>
            public DataCommandParameter Direction(ParameterDirection direction)
            {
                _param.Direction = direction;
                return this;
            }

            /// <summary>
            /// Sets the offset of the value property which has a default of <c>0</c>
            /// </summary>
            /// <param name="offset">The offset to assign.</param>
            /// <returns>A fluent version of this instance.</returns>
            public DataCommandParameter Offset(int offset)
            {
                _param.Offset = offset;
                return this;
            }

            /// <summary>
            /// Sets the parameters scale and percision.
            /// </summary>
            /// <param name="precision">The maximum number of digits used to represent the Value property.</param>
            /// <param name="scale">The number of decimal places to which Value is resolved.</param>
            /// <returns>
            /// A fluent version of this instance.
            /// </returns>
            public DataCommandParameter PrecisionScale(byte precision, byte scale)
            {
                _param.Precision = precision;
                _param.Scale = scale;
                return this;
            }

            /// <summary>
            /// Sets the maximum size, in bytes, of the data within the column which 
            /// has a default inferred by the value if not set.
            /// </summary>
            /// <param name="size">the maximum size, in bytes, of the data within the column.</param>
            /// <returns>
            /// A fluent version of this instance.
            /// </returns>
            public DataCommandParameter BinarySize(int size)
            {
                _param.Size = size;

                return this;
            }

            /// <summary>
            /// Sets the maximum size, in characters, of the string within the column which 
            /// has a default inferred by the value if not set.
            /// </summary>
            /// <param name="size">the maximum size, in characters, of the data within the column.</param>
            /// <returns>
            /// A fluent version of this instance.
            /// </returns>
            public DataCommandParameter StringSize(int size)
            {
                _param.Size = size * 2;

                return this;
            }

            /// <summary>
            /// Sets the paramters data type which has a default of <see cref="DataElementType.NVarChar"/>.
            /// </summary>
            /// <param name="dataType">Name of the parameter.</param>
            /// <returns>
            /// A fluent version of this instance.
            /// </returns>
            public DataCommandParameter DataType(DataElementType dataType)
            {  
                _param.SqlDbType = (SqlDbType)dataType;
                return this;
            }

            /// <summary>
            /// Sets the paramters value which has a default of <c>null</c>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="valueIsNull">if set to <c>true</c> value should be sent as null regarless of the object.</param>
            /// <returns>
            /// A fluent version of this instance.
            /// </returns>
            public DataCommandParameter Value(Object value, bool valueIsNull)
            {
                if (value == null || valueIsNull)
                {
                    _param.Value = DBNull.Value;
                }
                else
                {
                    //if (_param.SqlDbType == SqlDbType.NVarChar ||
                    //    _param.SqlDbType == SqlDbType.NChar ||
                    //    _param.SqlDbType == SqlDbType.NText)
                    //{
                    //    string text = (string)value;

                    //    if (text.Length > _param.Size * 2)
                    //    {
                    //        text = text.Substring(0, _param.Size * 2);
                    //    }
                    //}
                    //else if (_param.SqlDbType == SqlDbType.VarChar ||
                    //    _param.SqlDbType == SqlDbType.Char ||
                    //    _param.SqlDbType == SqlDbType.Text)
                    //{
                    //    string text = (string)value;

                    //    if (text.Length > _param.Size)
                    //    {
                    //        text = text.Substring(0, _param.Size);
                    //    }
                    //}

                    _param.Value = value;
                }

                return this;
            }
        }

        /// <summary>
        /// An result row from a data query
        /// </summary>
        public class DataCommandResultRow
        {
            private object[] _values;

            /// <summary>
            /// Initializes a new instance of the <see cref="DataCommandResultRow"/> class.
            /// </summary>
            /// <param name="values">The values.</param>
            public DataCommandResultRow(object[] values)
            {
                _values = values;
            }

            /// <summary>
            /// Gets the count of columns in the result.
            /// </summary>
            /// <value>
            /// The count of items in the result.
            /// </value>
            public int Count
            {
                get
                {
                    return _values.Length;
                }
            }

            /// <summary>
            /// Gets the column value at the specified index.
            /// </summary>
            /// <value>
            /// The column value.
            /// </value>
            /// <param name="index">The index.</param>
            /// <returns>the object at that index.</returns>
            public object this[int index]
            {
                get
                {
                    if (_values[index] == DBNull.Value)
                    {
                        return null;
                    }

                    return _values[index];
                }
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < _values.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append("; ");
                    }
                    sb.Append(_values[i].ToString());
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// A result from a data query
        /// </summary>
        public class DataCommandResultList : IEnumerable, IEnumerable<DataCommandResultRow>
        {
            private List<DataCommandResultRow> _values;

            /// <summary>
            /// Initializes a new instance of the <see cref="DataCommandResultList"/> class.
            /// </summary>
            public DataCommandResultList()
            {
                _values = new List<DataCommandResultRow>();
            }

            /// <summary>
            /// Gets the count of items in the result.
            /// </summary>
            /// <value>
            /// The count of items in the result.
            /// </value>
            public int Count
            {
                get
                {
                    return _values.Count;
                }
            }

            /// <summary>
            /// Gets the <see cref="DataCommandResultRow"/> at the specified index.
            /// </summary>
            /// <value>
            /// The <see cref="DataCommandResultRow"/>.
            /// </value>
            /// <param name="index">The index.</param>
            /// <returns>the object at that index.</returns>
            public DataCommandResultRow this[int index]
            {
                get
                {
                    return _values[index];
                }
            }

            /// <summary>
            /// Adds the specified row.
            /// </summary>
            /// <param name="row">The row.</param>
            public void Add(DataCommandResultRow row)
            {
                _values.Add(row);
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < 5 && i < _values.Count; i++)
                {
                    sb.AppendLine(_values[i].ToString());
                }

                if (_values.Count > 5)
                {
                    sb.AppendLine("....");
                }

                return sb.ToString();
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A enumerator that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<DataCommandResultRow> GetEnumerator()
            {
                return _values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _values.GetEnumerator();
            }
        }

        #endregion
    }
}
