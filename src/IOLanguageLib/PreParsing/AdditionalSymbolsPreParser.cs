using System;
using System.Collections.Generic;
using System.Numerics;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Exceptions;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    //TODO test
    //TODO тест: переменная в конце строки
    //TODO тесты на все ошибки
    public class AdditionalSymbolsPreParser : AbstractPreParser
    {
        protected override IEnumerable<Symbol> PreParse(PreParsingContext context)
        {
            while (context.GoRight())
            {
                var symbol = GetSymbol(context.CurrentSymbol, context);
                if (symbol is not Space)
                    yield return symbol;
            }
        }

        //TODO
        private static Symbol GetSymbol(Symbol currentSymbol, PreParsingContext context)
        {
            try
            {
                return currentSymbol switch
                {
                    Digit => GetIndividualConstant(context),
                    Letter letter => GetObjectVariable(letter, context),
                    EmptySymbol => throw new PreParsingException("Unexpected symbol.", context.Index),
                    ErrorSymbol => throw new PreParsingException("Unexpected symbol.", context.Index),
                    Underlining => throw new PreParsingException("Unexpected symbol.", context.Index),
                    null => throw new ArgumentNullException(nameof(currentSymbol)),
                    _ => currentSymbol
                };
            }
            catch (PreParsingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new PreParsingException("Other exception.", context.Index, e);
            }
        }

        private static IndividualConstant GetIndividualConstant(PreParsingContext context)
        {
            return GetInteger(context);
        }

        private static BigInteger GetInteger(PreParsingContext context)
        {
            if (context.CurrentSymbol is not Digit)
                throw new PreParsingException("Expected digit.", context.Index);

            var digit = context.CurrentSymbol as Digit;
            
            var integer = new BigInteger(digit);
            while (context.NextSymbol is Digit)
            {
                context.GoRight();
                digit = context.CurrentSymbol as Digit;
                integer = integer * 10 + digit;
            }

            return integer;
        }

        private static ObjectVariable GetObjectVariable(Letter letter, PreParsingContext context)
        {
            if (context.NextSymbol is not Underlining) 
                return new ObjectVariable(letter);
            
            if (!context.GoRight() || !context.GoRight())
                throw new PreParsingException("Expected index of variable after underlining.", context.Index);

            var integer = GetInteger(context);

            if (integer > uint.MaxValue)
                throw new PreParsingException($"Index should be no more then {uint.MaxValue}", context.Index);

            return new ObjectVariable(letter, (uint) integer);

        }
    }
}