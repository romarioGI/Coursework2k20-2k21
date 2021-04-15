using System;

namespace IOLanguageLib.Exceptions
{
    public class PreParsingException : Exception
    {
        public readonly int IndexOfError;

        public PreParsingException(string message, int indexOfError, Exception inner = null)
            : base(GetMessage(indexOfError, message), inner)
        {
            IndexOfError = indexOfError;
        }

        private static string GetMessage(int indexOfError, string message)
        {
            return $"at {indexOfError} symbol: {message}";
        }
    }
}