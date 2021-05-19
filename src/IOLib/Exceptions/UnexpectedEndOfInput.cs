using System;

namespace IOLib.Exceptions
{
    public class UnexpectedEndOfInput : InputException
    {
        public UnexpectedEndOfInput(string message = null, Exception inner = null) : base(GetMessage(message), inner)
        {
        }

        private static string GetMessage(string message)
        {
            var result = "Unexpected end of input.";

            if (message is not null)
                result = $"{result} {message}";

            return result;
        }
    }
}