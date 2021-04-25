using System;
using System.Collections.Generic;
using System.Numerics;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Exceptions;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
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

        private static Symbol GetSymbol(Symbol currentSymbol, PreParsingContext context)
        {
            try
            {
                return currentSymbol switch
                {
                    Digit => GetIndividualConstant(context),
                    Letter letter => GetObjectVariable(letter, context),
                    EmptySymbol => throw new UnexpectedCharacter(context.Index),
                    ErrorSymbol => throw new UnexpectedCharacter(context.Index),
                    Underlining => throw new UnexpectedCharacter(context.Index),
                    null => throw new ArgumentNullException(nameof(currentSymbol)),
                    _ => currentSymbol
                };
            }
            catch (InputException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new IndexedInputException(context.Index, "Other exception.", e);
            }
        }

        private static IndividualConstant GetIndividualConstant(PreParsingContext context)
        {
            return GetInteger(context);
        }

        private static BigInteger GetInteger(PreParsingContext context)
        {
            if (context.CurrentSymbol is not Digit)
                throw new UnexpectedCharacter(context.Index, "Expected digit.");

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
                throw new UnexpectedCharacter(context.Index, "Expected index of variable after underlining.");

            var integer = GetInteger(context);

            if (integer > uint.MaxValue)
                throw new UnexpectedCharacter(context.Index, $"Index should be no more then {uint.MaxValue}");

            return new ObjectVariable(letter, (uint) integer);
        }
    }
}