using System.Collections.Generic;
using System.Linq;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Input
{
    //TODO tests
    public class AutomatonTokenizer : ITokenizer
    {
        private static readonly EmptySymbol EmptySymbol = new();
        private readonly Automaton<char, Symbol> _tokensAutomaton = TokensAutomatonFactory.GetInstance();

        public IEnumerable<Symbol> Tokenize(string input)
        {
            _tokensAutomaton.Reset();

            //TODO check errors
            var result = _tokensAutomaton.Run(input)
                .Where(IsNotEmpty);

            return result;
        }

        private static bool IsNotEmpty(Symbol symbol)
        {
            return !symbol.Equals(EmptySymbol);
        }
    }
}