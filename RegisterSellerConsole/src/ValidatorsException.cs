using System;

namespace RegisterSellerConsole.src
{
    /// <summary>
    ///  Creates a validation exception, thrown in 2 methods in the Validations class
    /// </summary>
    public class ValidatorsException : Exception
    {
        public ValidatorsException() { }
        public ValidatorsException(string message) : base(message) { }
        public ValidatorsException(string message, Exception innerException) : base(message, innerException) { }

    }
}