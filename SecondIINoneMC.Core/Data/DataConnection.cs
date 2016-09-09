using System;
using System.Data;
using System.Data.SqlClient;

namespace Multiview.Data
{
    /// <summary>
    /// Represents a connection wrapper that helps prevent a lot repetitive code.
    /// </summary>
    /// <remarks>
    /// This class is used to handle the connection and transaction state used by 
    /// all classes.
    /// </remarks>
    public class DataConnection : Object, IDisposable
    {
        #region Const

        /// <summary>
        /// The default connection timeout.
        /// </summary>
        public const int DefaultConnectionTimeout = 15;

        #endregion

        #region Private Fields

        /// <summary>
        /// Holds the subscribers to the info message.
        /// </summary>
        private DataMessageEventHandler _infoMessage;

        /// <summary>
        /// The connection string used when openning the connection.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Holds the active connection. <c>null</c> is used to determine open state.
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// Used to hold an active transaction. <c>null</c> is used to determine scope.
        /// </summary>
        private SqlTransaction _transaction;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="connectionString">The connection string to be used to open the connection.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="connectionString"/> <c>null</c> or empty.
        /// </exception>
        public DataConnection(string connectionString)
            : this(connectionString, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to be used to open the connection.</param>
        /// <param name="autoOpen">if set to <c>true</c> open should be executed.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="connectionString"/> <c>null</c> or empty.
        /// </exception>
        /// <exception cref="DataConnectionException">
        /// thrown when the connection could not be opened.
        /// </exception>
        public DataConnection(string connectionString, bool autoOpen)
        {
            ArgumentValidator.ThrowOnNullOrEmpty("connectionString", connectionString);

            _infoMessage = null;
            _connection = null;
            _transaction = null;
            _connectionString = connectionString;

            if (autoOpen)
            {
                Open();
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DataConnection"/> class.
        /// </summary>
        ~DataConnection()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or 
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Dispose(bool managed)
        {
            if (managed)
            {
                Close();
            }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the Connection object for the data provider.
        /// </summary>
        internal SqlConnection InnerConnection
        {
            get
            {
                return _connection;
            }
        }

        /// <summary>
        /// Gets the Transaction object for the data provider.
        /// </summary>
        internal SqlTransaction InnerTransaction
        {
            get
            {
                return _transaction;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is available; otherwise, <c>false</c>.
        /// </value>
        public bool IsAvailable
        {
            get
            {
                return _connection != null &&
                       _connection.State == ConnectionState.Open;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is in transaction.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is in transaction; otherwise, <c>false</c>.
        /// </value>
        public bool IsInTransaction
        {
            get
            {
                return _transaction != null;
            }
        }

        /// <summary>
        /// Gets the time (in seconds) to wait while trying to establish a connection before terminating
        /// the attempt and generating an error.
        /// </summary>
        /// <value>
        /// The time (in seconds) to wait for a connection to open. 
        /// The default value is 15 seconds.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// thrown when <paramref name="value"/> is less than 1 or greater than <see cref="Int32.MaxValue"/>.
        /// </exception>
        public int ConnectionTimeout
        {
            get
            {
                StringHelper.TextBlock textBlock;

                if (StringHelper.TryFindTextBlock(_connectionString, "Connect Timeout=", ";", out textBlock))
                {
                    return Int32.Parse(textBlock.InnerText.Trim());
                }

                return DefaultConnectionTimeout;
            }
            set
            {
                ArgumentValidator.ThrowOnOutOfRange("value", value, 1, Int32.MaxValue);

                string newValue = "Connect Timeout=" + value + ";";

                StringHelper.TextBlock textBlock;

                if (StringHelper.TryFindTextBlock(_connectionString, "Connect Timeout=", ";", out textBlock))
                {
                    _connectionString = StringHelper.ReplaceTextBlock(textBlock, newValue);
                }
                else
                {
                    _connectionString += newValue;
                }
            }
        }

        /// <summary>
        /// Gets or sets the string used to open a database.
        /// </summary>
        /// <value>
        /// The connection string that includes the source database name, and other parameters
        /// needed to establish the initial connection. 
        /// </value>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Opens the database connection with the property settings set during construction.
        /// </summary>
        /// <exception cref="DataConnectionException">
        /// thrown when the connection could not be opened.
        /// </exception>
        public void Open()
        {
            ArgumentValidator.ThrowOnCondition(_connection != null, "Connection is already open.");

            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();

                if (_infoMessage != null)
                {
                    _connection.InfoMessage += HandleConnectionInfoMessage;
                }
            }
            catch (TimeoutException ex)
            {
                throw new DataConnectionException(ErrorType.ConnectionOpenTimeout,
                    "Timeout occured while openning the connection to the database.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DataConnectionException(ErrorType.ConnectionStringError,
                    "Connection string has errors.", ex);
            }
            catch (Exception ex)
            {
                throw new DataConnectionException(ErrorType.ConnectionOpenError,
                    "Unable to open connection to the database.", ex);
            }
        }

        /// <summary>
        /// Occurs when a message is sent from the connection such as print statements and errors.
        /// </summary>
        public event DataMessageEventHandler InfoMessage
        {
            add
            {
                if (value != null)
                {
                    if (_infoMessage == null && _connection != null)
                    {
                        _connection.InfoMessage += HandleConnectionInfoMessage;
                    }
                    _infoMessage += value;
                }
            }
            remove
            {
                _infoMessage -= value;
                if (_infoMessage == null)
                {
                    _connection.InfoMessage -= HandleConnectionInfoMessage;
                }
            }
        }

        /// <summary>
        /// Begins a transaction on the open connection object.
        /// </summary>
        /// <exception cref="DataConnectionException">
        /// Thrown when the connection has not been created yet or was lost,
        /// a transaction is already started.
        /// </exception>
        /// <remarks>
        /// If the connection object is not open this method has no affect.
        /// </remarks>
        public void BeginTransaction()
        {
            if (IsAvailable == false)
            {
                throw new DataConnectionException(ErrorType.ConnectionNotOpen,
                    "Connection must be opened first.");
            }

            try
            {
                _transaction = _connection.BeginTransaction();
            }
            catch (InvalidOperationException ex)
            {
                throw new DataConnectionException(ErrorType.TransactionAlreadyStarted,
                    "A transaction is currently active.", ex);
            }
            catch (Exception ex)
            {
                throw new DataConnectionException(ErrorType.TransactionBeginError,
                    "An error occurred while trying to begin the transaction.", ex);
            }
        }

        /// <summary>
        /// Rollback the pending transaction on the open connection object.
        /// </summary>
        /// <remarks>
        /// If a transaction has not be started, this method has no effect.
        /// </remarks>
        public void Rollback()
        {
            Rollback(false);
        }

        /// <summary>
        /// Rollback the pending transaction on the open connection object.
        /// </summary>
        /// <param name="throwOnError">if set to <c>true</c> exceptions are thrown when the roll back fails.</param>
        /// <exception cref="DataConnectionException">
        /// Thrown when transaction has already been committed, rolled back, or the connection was broken.
        /// </exception>
        /// <remarks>
        /// If a transaction has not be started, this method has no effect.
        /// </remarks>
        public void Rollback(bool throwOnError)
        {
            if (_transaction != null)
            {
                try
                {
                    if (IsAvailable)
                    {
                        _transaction.Rollback();
                    }

                    _transaction.Dispose();
                    _transaction = null;
                }
                catch (InvalidOperationException ex)
                {
                    if (throwOnError)
                    {
                        throw new DataConnectionException(ErrorType.TransactionRollbackError,
                            "The transaction has already been committed, rolled back, or the connection was broken.", ex);
                    }
                }
                catch (Exception ex)
                {
                    if (throwOnError)
                    {
                        throw new DataConnectionException(ErrorType.TransactionRollbackError,
                            "An error occurred while trying to commit the transaction.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Commit the pending transaction on the open connection object.
        /// </summary>
        /// <exception cref="DataConnectionException">
        /// The connection has not been created yet or was lost, 
        /// or the transaction has already been committed or rolled back.
        /// </exception>
        /// <remarks>
        /// If a transaction has not be started, this method has no affect.
        /// </remarks>
        public void Commit()
        {
            if (IsAvailable == false)
            {
                throw new DataConnectionException(ErrorType.ConnectionNotOpen,
                    "Connection must be opened first.");
            }

            if (_transaction != null)
            {
                try
                {
                    _transaction.Commit();

                    _transaction.Dispose();
                    _transaction = null;
                }
                catch (InvalidOperationException ex)
                {
                    throw new DataConnectionException(ErrorType.TransactionCommitError,
                        "Unable to commit transaction on the Host.", ex);
                }
                catch (Exception ex)
                {
                    throw new DataConnectionException(ErrorType.TransactionCommitError,
                        "An error occurred while trying to commit the transaction.", ex);
                }
            }
        }

        /// <summary>
        /// Closes the active connection.
        /// </summary>
        /// <remarks>
        /// Can be called multiple times with out harm. 
        /// If a transaction is pending the transaction will be rolled back. 
        /// NO Exceptions will be thrown from this method.
        /// </remarks>
        public void Close()
        {
            try
            {
                if (_connection != null)
                {
                    Rollback(false);

                    var conn = _connection;
                    _connection = null;

                    while (conn.State == ConnectionState.Connecting ||
                           conn.State == ConnectionState.Executing ||
                           conn.State == ConnectionState.Fetching)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }

        /// <summary>
        /// A simple way to execute a non-query statement against the current connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement.</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentException">
        /// thrown when <paramref name="sqlStatement"/> is <c>null</c> or empty.
        /// </exception>
        /// <exception cref="DataConnectionException">
        /// The connection has not been created yet or was lost.
        /// </exception>
        /// <exception cref="DataCommandException">
        /// An exception occurred while executing the command.
        /// </exception>
        public int SimpleExecuteNonQuery(string sqlStatement)
        {
            ArgumentValidator.ThrowOnNullOrEmpty("sqlStatement", sqlStatement);

            if (IsAvailable == false)
            {
                throw new DataConnectionException(ErrorType.ConnectionNotOpen,
                    "Connection must be opened first.");
            }

            int affected = 0;

            try
            {
                using (var sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = sqlStatement;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Connection = _connection;
                    sqlCommand.Transaction = _transaction;

                    int r = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new DataConnectionException(ErrorType.CommandExecuteError,
                    "Unable execute the simple command.", ex);
            }

            return affected;
        }


        /// <summary>
        /// A simple way to execute a non-query statement against the current connection
        /// and returns the number of rows affected.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement.</param>
        /// <param name="timeout"></param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentException">
        /// thrown when <paramref name="sqlStatement"/> is <c>null</c> or empty.
        /// </exception>
        /// <exception cref="DataConnectionException">
        /// The connection has not been created yet or was lost.
        /// </exception>
        /// <exception cref="DataCommandException">
        /// An exception occurred while executing the command.
        /// </exception>
        public int SimpleExecuteNonQuery(string sqlStatement, int timeout)
        {
            ArgumentValidator.ThrowOnNullOrEmpty("sqlStatement", sqlStatement);

            if (IsAvailable == false)
            {
                throw new DataConnectionException(ErrorType.ConnectionNotOpen,
                    "Connection must be opened first.");
            }

            int affected = 0;

            try
            {
                using (var sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandTimeout = timeout;
                    sqlCommand.CommandText = sqlStatement;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Connection = _connection;
                    sqlCommand.Transaction = _transaction;

                    int r = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new DataConnectionException(ErrorType.CommandExecuteError,
                    "Unable execute the simple command.", ex);
            }

            return affected;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Raises the <see cref="InfoMessage"/>.
        /// </summary>
        /// <param name="e">The event data.</param>
        private void OnInfoMessage(DataMessageEventArg e)
        {
            if (_infoMessage != null)
            {
                _infoMessage(this, e);
            }
        }

        /// <summary>
        /// Handles the connection info message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event data.</param>
        private void HandleConnectionInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            OnInfoMessage(new DataMessageEventArg(e.Message));
        }

        #endregion
    }
}
