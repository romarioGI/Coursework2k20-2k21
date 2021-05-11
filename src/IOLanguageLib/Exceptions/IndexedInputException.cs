using System;

namespace IOLanguageLib.Exceptions
{
    public class IndexedInputException : InputException
    {
        public readonly int IndexOfError;

        public IndexedInputException(int indexOfError, string message, Exception inner = null) : base(message, inner)
        {
            IndexOfError = indexOfError;
        }
    }
}