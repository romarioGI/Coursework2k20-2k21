using System;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Translating
{
    public interface IReportWriter
    {
        public void WriteError(string message, string input, Exception error);

        public void WriteError(string message, Symbol[] symbols, Exception error);

        public void WriteLine(string message);
    }
}