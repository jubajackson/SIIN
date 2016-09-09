using System;
using System.Collections.Generic;

namespace Multiview
{
    /// <summary>
    /// Use to pass the business rule error. Inherited from <see cref="Exception"/> so it can be thrown.
    /// The internal List implementaion has been shielded to aviod direct manupilations
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// The List of all the violated Business rules with param name and error messages encapsulated in <see cref="ValidationError"/>
        /// </summary>
        private List<ValidationError> _errors;
        
        /// <summary>
        /// Gets the errors. The internal List implementaion has been shielded to aviod direct manupilations
        /// </summary>
        /// <value>
        /// A non <c>Null</c> list of <see cref="ValidationError"/>
        /// </value>
        public IEnumerable<ValidationError> Errors
        {
            get
            {
                return _errors;
            }
        }

        /// <summary>
        /// Gets the Number of Errors.
        /// </summary>
        /// <value>
        /// An <see cref="int"/>
        /// </value>
        public int ErrorCount
        {
            get
            {
                return _errors.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has any validation errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has any validation errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasError
        {
            get
            {
                return _errors.Count > 0;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
        {
            _errors = new List<ValidationError>();
        }

        /// <summary>
        /// Adds the error to the list.
        /// </summary>
        /// <param name="parameterName">A non <c>Null</c> and non empty <see cref="string"/>. Name of the parameter which violates the business rule</param>
        /// <param name="errorMessage">A non <c>Null</c> and non empty <see cref="string"/>. The error message for the violation</param>
        ///<exception cref="ArgumentException">Thrown when <paramref name="errorMessage"/> is <c>null</c>, empty, or only whitespace.</exception>
        ///<exception cref="ArgumentException">Thrown when <paramref name="parameterName"/> is <c>null</c>, empty, or only whitespace.</exception>
        public void AddError(string errorMessage, string parameterName)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("errorMessage", errorMessage);
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("parameterName", parameterName);
            _errors.Add(new ValidationError(parameterName, errorMessage));
        }
    }
}