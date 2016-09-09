using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondIINoneMC.Core.Helpers
{
    /// <summary>
    /// This class was built to provide consitent messages across exceptions.
    /// </summary>
    public static class ExceptionMessage
    {
        /// <summary>
        /// Builds the exception message for a null reference exception coming from a <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="argumentName">Name of the argument that caused the exception.</param>
        /// <returns>Returns the complete exception message.</returns>
        public static string ArgumentNullExceptionMessage(string argumentName)
        {
            return NonNullReferenceMessage(argumentName) +
                   "\r\nParameter name: " + argumentName;
        }

        /// <summary>
        /// Builds the exception message for a null reference exception.
        /// </summary>
        /// <param name="argumentName">Name of the argument that caused the exception.</param>
        /// <returns>Returns the exception message.</returns>
        public static string NonNullReferenceMessage(string argumentName)
        {
            return "Reference object \"" + argumentName + "\" cannot be null.";
        }

        /// <summary>
        /// Builds the exception message for a null reference exception.
        /// </summary>
        /// <param name="argumentName">Name of the argument that caused the exception.</param>
        /// <returns>Returns the exception message.</returns>
        public static string NonNullOrEmptyStringMessage(string argumentName)
        {
            return "string cannot be null or empty string.\r\nParameter name: " + argumentName;
        }

        /// <summary>
        /// Builds the exception message for a null, empty or whitespace exception message.
        /// </summary>
        /// <param name="argumentName">Name of the argument that caused the exception.</param>
        /// <returns>Returns the exception message.</returns>
        public static string NonNullEmptyOrWhitespaceMessage(string argumentName)
        {
            return "string cannot be null, empty, or only whitespace.\r\nParameter name: " + argumentName;
        }

        /// <summary>
        /// Builds the exception message for a failed <see cref="IComparable"/> and <see cref="IComparable&lt;T&gt;"/> when there is a type miss match.
        /// </summary>
        /// <param name="type">The object type which is expected.</param>
        /// <returns>Returns the exception message.</returns>
        public static string ComparableTypeMissMatchMessage(Type type)
        {
            return "Object must be of type " + type.Name + ".";
        }
    }
}
