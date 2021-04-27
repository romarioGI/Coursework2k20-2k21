using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.Contexts
{
    public class SymbolContextWithPrevious : SymbolContext
    {
        public SymbolContextWithPrevious(IEnumerable<Symbol> symbols) : base(symbols)
        {
            PreviousSymbol = null;
        }

        public Symbol PreviousSymbol { get; private set; }

        protected override Symbol Next()
        {
            PreviousSymbol = CurrentSymbol;
            var currentSymbol = base.Next();

            return currentSymbol;
        }
    }
}