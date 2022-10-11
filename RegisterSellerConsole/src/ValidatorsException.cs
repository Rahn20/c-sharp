using System;


namespace RegisterSellerConsole.src
{
    /**
     * <summary>
     *      Creates validation exception, the exception has been thrown in 2 methods in Validations class.
     * </summary>
     */
    public class ValidatorsException : Exception
    {
        public ValidatorsException() { }
        public ValidatorsException(string message) : base(message) { }
        public ValidatorsException(string message, Exception innerException) : base(message, innerException) { }

    }
}
