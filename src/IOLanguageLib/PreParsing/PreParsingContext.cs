using System.Collections.Generic;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class PreParsingContext : IContext
    {
        private static readonly EmptySymbol EmptySymbol = new();

        private readonly IEnumerator<Symbol> _enumerator;

        public PreParsingContext(IEnumerable<Symbol> symbols)
        {
            _enumerator = symbols.GetEnumerator();
            IsOver = false;
            GoRight();
        }

        public Symbol CurrentSymbol { get; private set; }

        public bool IsOver { get; private set; }

        public void GoRight()
        {
            if (_enumerator.MoveNext())
            {
                CurrentSymbol = _enumerator.Current;
            }
            else
            {
                IsOver = true;
                CurrentSymbol = EmptySymbol;
            }
        }
    }
}