﻿using IOLanguageLib.Words;

namespace IOLanguageLib.Translating
{
    public interface ITranslator
    {
        public Formula Translate(string input);
    }
}