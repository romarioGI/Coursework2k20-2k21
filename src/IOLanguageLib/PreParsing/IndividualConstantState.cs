using System.Numerics;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class IndividualConstantState : IState<PreParsingContext, Symbol>
    {
        public (IState<PreParsingContext, Symbol>, Symbol) Next(PreParsingContext input)
        {
            IState<PreParsingContext, Symbol> nextState;
            Symbol output;

            if (!input.IsOver && input.CurrentSymbol is Digit)
            {
                nextState = PreParserAutomatonFactory.InitialState;
                output = new IndividualConstant(GetInteger(input));
            }
            else
            {
                nextState = PreParserAutomatonFactory.ErrorState;
                output = PreParserAutomatonFactory.ErrorSymbol;
            }

            return (nextState, output);
        }

        private static BigInteger GetInteger(PreParsingContext input)
        {
            var result = new BigInteger();
            while (!input.IsOver && input.CurrentSymbol is Digit digit)
            {
                result += digit;
                input.GoRight();
            }

            return result;
        }
    }
}