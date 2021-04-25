using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class PreParsingContext
    {
        private readonly IEnumerator<Symbol> _enumerator;

        public PreParsingContext(IEnumerable<Symbol> symbols)
        {
            _enumerator = symbols.GetEnumerator();
            PreviousSymbol = null;
            CurrentSymbol = null;
            NextSymbol = Next();
            Index = -1;
        }

        public Symbol CurrentSymbol { get; private set; }

        public Symbol NextSymbol { get; private set; }
        
        public Symbol PreviousSymbol { get; private set; }

        public int Index { get; set; }

        public bool GoRight()
        {
            ++Index;
            PreviousSymbol = CurrentSymbol;
            CurrentSymbol = NextSymbol;
            NextSymbol = Next();

            return CurrentSymbol is not null;
        }

        private Symbol Next()
        {
            return _enumerator.MoveNext() ? _enumerator.Current : null;
        }
    }
}