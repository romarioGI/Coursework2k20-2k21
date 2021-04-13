using System;

namespace IOLanguageLib.Exceptions
{
    public class UnexpectedEndOfInput : TokenizingException
    {
        public UnexpectedEndOfInput(Exception inner = null) : base("Unexpected end of input.", inner)
        {
        }
    }
}