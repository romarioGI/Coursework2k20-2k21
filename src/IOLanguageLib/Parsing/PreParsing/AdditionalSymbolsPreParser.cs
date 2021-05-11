using System;
using System.Collections.Generic;
using System.Numerics;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Exceptions;
using IOLanguageLib.Parsing.Contexts;

namespace IOLanguageLib.Parsing.PreParsing
{
    public class AdditionalSymbolsPreParser : PreParser
    {
        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            var context = new SymbolContextWithNext(input);

            return PreParse(context);
        }

        private static IEnumerable<Symbol> PreParse(SymbolContextWithNext context)
        {
            while (context.MoveNext())
            {
                var symbol = GetSymbol(context.CurrentSymbol, context);
                if (symbol is not Space)
                    yield return symbol;
            }
        }

        private static Symbol GetSymbol(Symbol currentSymbol, SymbolContextWithNext context)
        {
            try
            {
                return currentSymbol switch
                {
                    Digit => GetIndividualConstant(context),
                    Letter letter => GetObjectVariable(letter, context),
                    EmptySymbol or ErrorSymbol or Underlining => throw new UnexpectedSymbol(context.Index),
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

        private static IndividualConstant GetIndividualConstant(SymbolContextWithNext context)
        {
            return GetInteger(context);
        }

        private static BigInteger GetInteger(SymbolContextWithNext context)
        {
            if (context.CurrentSymbol is not Digit digit)
                throw new UnexpectedSymbol(context.Index, "Expected digit.");

            var integer = new BigInteger(digit);
            var ten = new BigInteger(10);
            while (context.NextSymbol is Digit)
            {
                context.MoveNext();
                digit = context.CurrentSymbol as Digit;
                integer = integer * ten + digit;
            }

            return integer;
        }

        private static ObjectVariable GetObjectVariable(Letter letter, SymbolContextWithNext context)
        {
            if (context.NextSymbol is not Underlining)
                return new ObjectVariable(letter);

            if (!context.MoveNext() || !context.MoveNext())
                throw new UnexpectedSymbol(context.Index, "Expected index of variable after underlining.");

            var integer = GetInteger(context);

            if (integer > uint.MaxValue)
                throw new UnexpectedSymbol(context.Index, $"Index should be no more then {uint.MaxValue}.");

            return new ObjectVariable(letter, (uint) integer);
        }
    }
}