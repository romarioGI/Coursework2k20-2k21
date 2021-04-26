using System;
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
            return input
                .Select(ToSymbol)
                .Select(ThrowIfError)
                .Where(IsNotEmptySymbol)
                .Finally(ThrowIfNotInFinalState)
                .Finally(ResetAutomaton);
        }

        private Symbol ToSymbol(char c, int index)
        {
            try
            {
                return _tokensAutomaton.Next(c);
            }
            catch (Exception e)
            {
                throw new IndexedInputException(index, "Other error.", e);
            }
        }

        private static Symbol ThrowIfError(Symbol symbol, int index)
        {
            if (symbol is ErrorSymbol)
                throw new UnexpectedSymbol(index);

            return symbol;
        }

        private static bool IsNotEmptySymbol(Symbol symbol)
        {
            return !(symbol is EmptySymbol);
        }

        private void ThrowIfNotInFinalState()
        {
            if (!_tokensAutomaton.InFinalState)
                throw new UnexpectedEndOfInput();
        }

        private void ResetAutomaton()
        {
            _tokensAutomaton.Reset();
        }
    }
}