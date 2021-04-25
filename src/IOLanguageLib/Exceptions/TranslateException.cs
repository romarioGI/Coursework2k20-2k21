using System;

namespace IOLanguageLib.Exceptions
{
    public class TranslateException: InputException
    {
        public TranslateException() : base("Translate exception. See report.")
        {
        }
    }
}