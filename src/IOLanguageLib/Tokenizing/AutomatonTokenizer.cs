using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using IOLanguageLib.Exceptions;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Tokenizing
{
    public class AutomatonTokenizer : ITokenizer
    {
        private readonly Automaton<char, Symbol> _tokensAutomaton = TokensAutomatonFactory.GetInstance();

        public IEnumerable<Symbol> Tokenize(string input)
        {
            var result = _tokensAutomaton.Run(input)
                .Select(ThrowIfError)
                .Where(IsNotEmptySymbol);

            foreach (var symbol in result)
                yield return symbol;

            if (!_tokensAutomaton.InFinalState)
                throw new UnexpectedEndOfInput();
            _tokensAutomaton.Reset();
        }

        private static bool IsNotEmptySymbol(Symbol symbol)
        {
            return !(symbol is EmptySymbol);
        }

        private static Symbol ThrowIfError(Symbol symbol, int index)
        {
            if (symbol is ErrorSymbol)
                throw new UnexpectedCharacter(index);

            return symbol;
        }
    }
}