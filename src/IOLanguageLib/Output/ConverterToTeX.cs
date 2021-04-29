using System;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;

namespace IOLanguageLib.Output
{
    //TODO test
    public class ConverterToTeX : IConverter<Formula, string>, IConverter<Symbol, string>
    {
        private static readonly ConverterToSymbols Converter = new();

        public string Convert(Formula formula)
        {
            var symbols = Converter.Convert(formula);
            var strings = symbols.Select(Convert);
            var result = string.Join("", strings);

            return result;
        }
        
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