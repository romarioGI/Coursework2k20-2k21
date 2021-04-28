using System;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Output
{
    public class SymbolConverterLikeTeX : IConverter<Symbol, string>
    {
        public string Convert(Symbol symbol)
        {
            return symbol switch
            {
                Addition => "+",
                Comma => ",",
                Conjunction => "\\land",
                Disjunction => "\\lor",
                Division => "/",
                EqualityPredicate => "=",
                ExistentialQuantifier => "\\exists",
                Exponentiation => "^",
                False => "FALSE",
                Implication => "\\to",
                IndividualConstant c => c.Value.ToString(),
                LeftBracket => "(",
                LessPredicate => "<",
                MorePredicate => ">",
                Multiplication => "\\cdot",
                Negation => "\\lnot",
                ObjectVariable o => $"{o.Char}{(o.Index is null ? "" : $"_{o.Index}")}",
                RightBracket => ")",
                Space => " ",
                Subtraction => "-",
                True => "TRUE",
                UnaryMinus => "-",
                UniversalQuantifier => "\\forall",
                _ => throw new NotSupportedException()
            };
        }
    }
}