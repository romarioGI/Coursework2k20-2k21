using System;

namespace IOLanguageLib.Exceptions
{
    public class UnexpectedSymbol : IndexedInputException
    {
        public UnexpectedSymbol(int indexOfError, string message = null, Exception inner = null)
            : base(indexOfError, GetMessage(message), inner)
        {
        }

        private static string GetMessage(string message)
        {
            var result = "Unexpected symbol.";

            if (message is not null)
                result = $"{result} {message}";

            return result;
        }
    }
}