using System.Collections.Generic;
using IOLanguageLib.Alphabet;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    //TODO tests
    public class MinusPreParser: AbstractPreParser
    {
        protected override IEnumerable<Symbol> PreParse(PreParsingContext context)
        {
            while (context.GoRight())
            {
                if (context.CurrentSymbol is Minus)
                    yield return GetMinus(context.PreviousSymbol);
                yield return context.CurrentSymbol;
            }
        }
        
        private static readonly UnaryMinus UnaryMinus = new();
        private static readonly Subtraction BinaryMinus = new();
        
        private static Symbol GetMinus(Symbol previousSymbol)
        {
            if (previousSymbol is ObjectVariable || previousSymbol is IndividualConstant ||
                previousSymbol is RightBracket)
                return BinaryMinus;
            return UnaryMinus;
        }
    }
}