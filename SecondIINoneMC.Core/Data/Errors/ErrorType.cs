
namespace Multiview.Data
{
    /// <summary>
    /// Represents the type of error
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// The source was unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The connection string has an error
        /// </summary>
        ConnectionStringError = 1,
        /// <summary>
        /// The connection open call timed out
        /// </summary>
        ConnectionOpenTimeout = 2,
        /// <summary>
        /// The connection was not open
        /// </summary>
        ConnectionNotOpen,
        /// <summary>
        /// The transaction was already started
        /// </summary>
        TransactionAlreadyStarted,
        /// <summary>
        /// A connection open error
        /// </summary>
        ConnectionOpenError,
        /// <summary>
        /// The begin transaction error
        /// </summary>
        TransactionBeginError,
        /// <summary>
        /// The rollback transaction error
        /// </summary>
        TransactionRollbackError,
        /// <summary>
        /// The commit transaction error
        /// </summary>
        TransactionCommitError,
        /// <summary>
        /// The get command error
        /// </summary>
        CommandGetError,
        /// <summary>
        /// The get last identity command error
        /// </summary>
        CommandGetLastIdentity,
        /// <summary>
        /// The execute scalar command error 
        /// </summary>
        CommandExecuteScalarError,
        /// <summary>
        /// The execute command error
        /// </summary>
        CommandExecuteError,
    }
}
