using System;

namespace IOLanguageLib.Exceptions
{
    public class IndexedInputException : InputException
    {
        public readonly int IndexOfError;
        
        public IndexedInputException(int indexOfError, string message, Exception inner) : base(
            GetMessage(indexOfError, message), inner)
        {
            IndexOfError = indexOfError;
        }

        private static string GetMessage(int indexOfError, string message)
        {
            return $"at {indexOfError}:\t{message}";
        }
    }
}