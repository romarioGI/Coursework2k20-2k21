using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing
{
    public class SymbolContext
    {
        private readonly IEnumerator<Symbol> _enumerator;

        public SymbolContext(IEnumerable<Symbol> symbols)
        {
            _enumerator = symbols.GetEnumerator();
            CurrentSymbol = null;
            Index = -1;
        }

        public Symbol CurrentSymbol { get; private set; }

        public int Index { get; private set; }

        public bool MoveNext()
        {
            ++Index;
            CurrentSymbol = Next();

            return CurrentSymbol is not null;
        }

        protected virtual Symbol Next()
        {
            return _enumerator.MoveNext() ? _enumerator.Current : null;
        }
    }
}