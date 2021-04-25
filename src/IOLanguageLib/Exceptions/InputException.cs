using System;

namespace IOLanguageLib.Exceptions
{
    public abstract class InputException : Exception
    {
        protected InputException(string message, Exception inner = null) : base(message, inner)
        {
        }
    }
}