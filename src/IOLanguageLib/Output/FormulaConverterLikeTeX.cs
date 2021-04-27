using System;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;

namespace IOLanguageLib.Output
{
    //TODO test
    public class FormulaConverterLikeTeX : IFormulaConverter<string>
    {
        public string Convert(Formula formula)
        {
            return string.Join("", formula
                .Select(ToString));
        }

        private static string ToString(Symbol symbol)
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
                IndividualConstant => throw new NotImplementedException(),
                LeftBracket => "(",
                LessPredicate => "<",
                MorePredicate => ">",
                Multiplication => "\\cdot",
                Negation => "\\lnot",
                ObjectVariable => throw new NotImplementedException(),
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