using System;

namespace IOLanguageLib.Exceptions
{
    public class IndexedInputException : InputException
    {
        public readonly int IndexOfError;
        public readonly string InnerMessage;

        public IndexedInputException(int indexOfError, string message, Exception inner = null) : base(
            GetMessage(indexOfError, message), inner)
        {
            IndexOfError = indexOfError;
            InnerMessage = message;
        }

        private static string GetMessage(int indexOfError, string message)
        {
            return $"at {indexOfError}:\t{message}";
        }
    }
}