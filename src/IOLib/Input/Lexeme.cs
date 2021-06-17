using System;
using IOLib.Language;

namespace IOLib.Input
{
    public class Lexeme
    {
        public Lexeme(Symbol symbol, string @string)
        {
            Symbol = symbol ?? throw new NullReferenceException();
            String = @string ?? throw new NullReferenceException();
        }

        public string String { get; }

        public Symbol Symbol { get; }

        public int Length => String.Length;
    }
}