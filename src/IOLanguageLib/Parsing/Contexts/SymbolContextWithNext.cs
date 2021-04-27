using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.Contexts
{
    public class SymbolContextWithNext: SymbolContext
    {
        public SymbolContextWithNext(IEnumerable<Symbol> symbols): base(symbols)
        {
            NextSymbol = base.Next();
        }

        public Symbol NextSymbol { get; private set; }

        protected override Symbol Next()
        {
            var currentSymbol = NextSymbol;
            NextSymbol = base.Next();

            return currentSymbol;
        }
    }
}