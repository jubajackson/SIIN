using System;

namespace Multiview.Data
{
    /// <summary>
    /// Represents an exception that occurred in a <see cref="DataCommand"/> or <see cref="DataConnection"/>
    /// </summary>
    [Serializable]
    public class BaseDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataConnectionException" /> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public BaseDataException(ErrorType errorType, string message, Exception inner) : base(message, inner)
        {
            ErrorType = errorType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataException"/> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="message">The message.</param>
        public BaseDataException(ErrorType errorType, string message) : base(message)
        {
            ErrorType = errorType;
        }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public ErrorType ErrorType
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ExceptionMessage.Format(this);
        }
    }
}
