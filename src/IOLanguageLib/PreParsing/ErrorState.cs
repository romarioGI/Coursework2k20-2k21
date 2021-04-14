using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class ErrorState : IState<PreParsingContext, Symbol>
    {
        public (IState<PreParsingContext, Symbol>, Symbol) Next(PreParsingContext input)
        {
            input.GoRight();
            return (this, PreParserAutomatonFactory.EmptySymbol);
        }
    }
}