using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Input
{
    //TODO tests
    public class AutomatonTokenizer : ITokenizer
    {
        private readonly Automaton<char, Symbol> _tokensAutomaton = TokensAutomatonFactory.GetInstance();

        public IEnumerable<Symbol> Tokenize(string input)
        {
            _tokensAutomaton.Reset();

            //TODO add error handling, maybe output ErrorSymbol in automaton
            return _tokensAutomaton.Run(input);
        }
    }
}