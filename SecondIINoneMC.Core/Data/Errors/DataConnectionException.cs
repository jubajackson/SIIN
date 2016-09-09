using System;

namespace Multiview.Data
{
    /// <summary>
    /// Represents an exception that occurred in a <see cref="DataConnection"/>
    /// </summary>
    [Serializable]
    public class DataConnectionException : BaseDataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataConnectionException" /> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public DataConnectionException(ErrorType errorType, string message, Exception inner) : base(errorType, message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataConnectionException"/> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="message">The message.</param>
        public DataConnectionException(ErrorType errorType, string message)
            : base(errorType, message)
        {
        }
    }
}
