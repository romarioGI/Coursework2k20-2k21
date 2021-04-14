using System.Numerics;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class ObjectVariableState : IState<PreParsingContext, Symbol>
    {
        public (IState<PreParsingContext, Symbol>, Symbol) Next(PreParsingContext input)
        {
            IState<PreParsingContext, Symbol> nextState;
            Symbol output;

            if (!input.IsOver && input.CurrentSymbol is Letter letter)
            {
                input.GoRight();
                (nextState, output) = GetObjectVariable(input, letter);
            }
            else
            {
                nextState = PreParserAutomatonFactory.ErrorState;
                output = PreParserAutomatonFactory.ErrorSymbol;
            }

            return (nextState, output);
        }

        private static (IState<PreParsingContext, Symbol>, Symbol) GetObjectVariable(PreParsingContext input,
            Letter letter)
        {
            IState<PreParsingContext, Symbol> nextState;
            Symbol output;

            if (!input.IsOver && input.CurrentSymbol is Underlining)
            {
                input.GoRight();
                (nextState, output) = GetWithIndex(input, letter);
            }
            else
            {
                nextState = PreParserAutomatonFactory.InitialState;
                output = new ObjectVariable(letter);
            }

            return (nextState, output);
        }

        private static (IState<PreParsingContext, Symbol>, Symbol) GetWithIndex(PreParsingContext input, Letter letter)
        {
            IState<PreParsingContext, Symbol> nextState;
            Symbol output;

            if (!input.IsOver && input.CurrentSymbol is Digit)
            {
                (nextState, output) = GetWithSmallIndex(input, letter);
            }
            else
            {
                nextState = PreParserAutomatonFactory.ErrorState;
                output = PreParserAutomatonFactory.ErrorSymbol;
            }

            return (nextState, output);
        }

        private static (IState<PreParsingContext, Symbol>, Symbol) GetWithSmallIndex(PreParsingContext input,
            Letter letter)
        {
            IState<PreParsingContext, Symbol> nextState;
            Symbol output;

            var integer = GetInteger(input);
            if (integer <= uint.MaxValue)
            {
                var index = (uint) integer;
                nextState = PreParserAutomatonFactory.InitialState;
                output = new ObjectVariable(letter, index);
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