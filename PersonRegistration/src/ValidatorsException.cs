using System;

namespace PersonRegistration.src
{
    /**
     * <summary>
     *      Creates validation exception, the exception has been thrown in 1 method in Validations class.
     * </summary>
     */

    /// <summary>
    ///  Creates a validation exception, thrown in one method in the Validations class
    /// </summary>
    internal class ValidatorsException : Exception
    {
        public ValidatorsException() { }
        public ValidatorsException(string message) : base(message) { }
        public ValidatorsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
