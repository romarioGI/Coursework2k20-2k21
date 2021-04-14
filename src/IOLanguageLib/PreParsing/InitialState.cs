using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class InitialState : IState<PreParsingContext, Symbol>
    {
        public (IState<PreParsingContext, Symbol>, Symbol) Next(PreParsingContext input)
        {
            var currentSymbol = input.CurrentSymbol;
            switch (currentSymbol)
            {
                case Digit:
                    return (PreParserAutomatonFactory.IndividualConstantState,
                        PreParserAutomatonFactory.BetweenPreParsingStateSymbol);
                case Letter:
                    return (PreParserAutomatonFactory.ObjectVariableState,
                        PreParserAutomatonFactory.BetweenPreParsingStateSymbol);
                case Minus:
                    return (PreParserAutomatonFactory.MinusState, PreParserAutomatonFactory.EmptySymbol);
                case Space:
                    input.GoRight();
                    return (this, PreParserAutomatonFactory.EmptySymbol);
                case Underlining:
                    input.GoRight();
                    return (PreParserAutomatonFactory.ErrorState, PreParserAutomatonFactory.ErrorSymbol);
                default:
                    input.GoRight();
                    return (this, currentSymbol);
            }
        }
    }
}