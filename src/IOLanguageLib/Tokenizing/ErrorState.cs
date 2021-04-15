using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Tokenizing
{
    internal class ErrorState : IState<char, Symbol>
    {
        private static readonly EmptySymbol EmptySymbol = new();

        public (IState<char, Symbol>, Symbol) Next(char input)
        {
            return (this, EmptySymbol);
        }
    }
}