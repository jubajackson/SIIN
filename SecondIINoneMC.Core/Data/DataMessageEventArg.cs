using System;

namespace Multiview.Data
{
    /// <summary>
    /// Represents the method that will handle <see cref="DataConnection.InfoMessage"/> event
    /// of the <see cref="DataConnection"/>.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event data.</param>
    public delegate void DataMessageEventHandler(object sender, DataMessageEventArg e);

    /// <summary>
    /// Provides data for the <see cref="DataConnection.InfoMessage"/> event.
    /// </summary>
    public class DataMessageEventArg : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMessageEventArg"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DataMessageEventArg(string message)
        {
            Message = message ?? String.Empty;
        }

        /// <summary>
        /// Gets the message from the database.
        /// </summary>
        /// <value>
        /// The message from the database.
        /// </value>
        public string Message
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
            return Message;
        }
    }
}
