using System;

namespace IOLanguageLib.Exceptions
{
    public class TokenizingException : Exception
    {
        public TokenizingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}