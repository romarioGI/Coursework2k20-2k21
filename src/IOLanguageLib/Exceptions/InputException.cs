using System;

namespace IOLanguageLib.Exceptions
{
    public class InputException : Exception
    {
        public readonly int IndexOfError;
        
        public InputException(int indexOfError, string message, Exception inner) : base(message, inner)
        {
            IndexOfError = indexOfError;
        }
    }
}