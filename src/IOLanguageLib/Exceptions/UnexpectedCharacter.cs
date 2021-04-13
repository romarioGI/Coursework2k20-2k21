using System;

namespace IOLanguageLib.Exceptions
{
    public class UnexpectedCharacter: TokenizingException
    {
        public readonly int IndexOfError;

        public UnexpectedCharacter(int indexOfError, Exception inner = null)
            : base(GetMessage(indexOfError), inner)
        {
            IndexOfError = indexOfError;
        }

        private static string GetMessage(int indexOfError)
        {
            return $"Unexpected character with index {indexOfError}.";
        }
    }
}