using System;

namespace IOLanguageLib.Exceptions
{
    public class UnexpectedCharacter : InputException
    {
        public UnexpectedCharacter(int indexOfError, string message = null, Exception inner = null)
            : base(indexOfError, GetMessage(indexOfError, message), inner)
        {
        }

        private static string GetMessage(int indexOfError, string message)
        {
            var result = $"Unexpected character with index {indexOfError}.";

            if (message is not null)
                result = $"{result} {message}";

            return result;
        }
    }
}