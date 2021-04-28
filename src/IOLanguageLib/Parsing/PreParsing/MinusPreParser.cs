using System.Collections.Generic;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Parsing.Contexts;

namespace IOLanguageLib.Parsing.PreParsing
{
    public class MinusPreParser : PreParser
    {
        private static readonly UnaryMinus UnaryMinus = new();
        private static readonly Subtraction BinaryMinus = new();

        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            var context = new SymbolContextWithPrevious(input);

            return PreParse(context);
        }

        private static IEnumerable<Symbol> PreParse(SymbolContextWithPrevious context)
        {
            while (context.MoveNext())
            {
                if (context.CurrentSymbol is Minus)
                    yield return GetMinus(context.PreviousSymbol);
                yield return context.CurrentSymbol;
            }
        }

        private static Symbol GetMinus(Symbol previousSymbol)
        {
            if (previousSymbol is ObjectVariable or IndividualConstant or RightBracket)
                return BinaryMinus;
            return UnaryMinus;
        }
    }
}