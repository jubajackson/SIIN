using System;

namespace Multiview.Data
{
    /// <summary>
    /// Represents an exception that occurred in a <see cref="DataCommand"/>
    /// </summary>
    [Serializable]
    public class DataCommandException : BaseDataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataCommandException" /> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public DataCommandException(ErrorType errorType, string message, Exception inner) : base(errorType, message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCommandException"/> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="message">The message.</param>
        public DataCommandException(ErrorType errorType, string message)
            : base(errorType, message)
        {
        }
    }
}
