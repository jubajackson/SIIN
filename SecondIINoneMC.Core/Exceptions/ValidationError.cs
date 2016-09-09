namespace Multiview
{
    /// <summary>
    /// A class to hold the business logic validation errors
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets the name of the parameter which violates the business rule
        /// </summary>
        /// <value>
        /// A non <c>Null</c> <see cref="string"/>
        /// </value>
        public string ParameterName
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the error message for the violation
        /// </summary>
        /// <value>
        /// A non <c>Null</c> <see cref="string"/>
        /// </value>
        public string ErrorMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter which violates the business rule.</param>
        /// <param name="errorMessage">The error message for the violation.</param>
        public ValidationError(string parameterName, string errorMessage)
        {
            this.ParameterName = parameterName;
            this.ErrorMessage = errorMessage;
        }
    }
}