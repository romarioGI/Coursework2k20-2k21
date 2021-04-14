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
            IState<PreParsingContext, Symbol> nextState;
            Symbol output;
            switch (currentSymbol)
            {
                case Digit:
                    nextState = PreParserAutomatonFactory.IndividualConstantState;
                    output = PreParserAutomatonFactory.EmptySymbol;
                    break;

                case EmptySymbol:
                    nextState = PreParserAutomatonFactory.ErrorState;
                    output = PreParserAutomatonFactory.ErrorSymbol;
                    break;

                case ErrorSymbol:
                    nextState = PreParserAutomatonFactory.ErrorState;
                    output = PreParserAutomatonFactory.ErrorSymbol;
                    break;

                case Letter:
                    nextState = PreParserAutomatonFactory.ObjectVariableState;
                    output = PreParserAutomatonFactory.EmptySymbol;
                    break;

                case Space:
                    input.GoRight();
                    nextState = this;
                    output = PreParserAutomatonFactory.EmptySymbol;
                    break;

                case Underlining:
                    input.GoRight();
                    nextState = PreParserAutomatonFactory.ErrorState;
                    output = PreParserAutomatonFactory.ErrorSymbol;
                    break;

                default:
                    input.GoRight();
                    nextState = this;
                    output = currentSymbol;
                    break;
            }

            return (nextState, output);
        }
    }
}