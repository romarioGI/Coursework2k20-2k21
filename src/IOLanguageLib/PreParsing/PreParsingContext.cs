using System.Collections.Generic;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class PreParsingContext : IContext
    {
        private readonly IEnumerator<Symbol> _enumerator;

        public PreParsingContext(IEnumerable<Symbol> symbols)
        {
            _enumerator = symbols.GetEnumerator();
            IsOver = false;
            CurrentSymbol = null;
            GoRight();
        }

        public Symbol CurrentSymbol { get; private set; }

        public Symbol PreviousSymbol { get; private set; }

        public bool IsOver { get; private set; }

        public void GoRight()
        {
            PreviousSymbol = CurrentSymbol;
            if (_enumerator.MoveNext())
            {
                CurrentSymbol = _enumerator.Current;
            }
            else
            {
                IsOver = true;
                CurrentSymbol = null;
            }
        }
    }
}