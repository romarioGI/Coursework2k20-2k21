using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Output
{
    //TODO test
    public class FormulaPrinterLikeTeX : IFormulaPrinter
    {
        private static readonly LeftBracket LeftBracket = new();
        private static readonly RightBracket RightBracket = new();
        private static readonly Space Space = new();

        public string Print(Formula formula)
        {
            return string.Join("", ToSymbols(formula)
                .Select(ToString));
        }

        private static IEnumerable<Symbol> ToSymbols(Formula formula)
        {
            return formula switch
            {
                PredicateFormula predicateFormula => ToSymbols(predicateFormula),
                PropositionalConnectiveFormula propositionalConnectiveFormula => ToSymbols(
                    propositionalConnectiveFormula),
                QuantifierFormula quantifierFormula => ToSymbols(quantifierFormula),
                _ => throw new NotSupportedException()
            };
        }

        private static IEnumerable<Symbol> ToSymbols(PredicateFormula formula)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Symbol> ToSymbols(PropositionalConnectiveFormula formula)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Symbol> ToSymbols(QuantifierFormula formula)
        {
            yield return formula.Quantifier;
            yield return formula.ObjectVariable;
            yield return LeftBracket;
            foreach (var symbol in ToSymbols(formula.SubFormula))
                yield return symbol;
            yield return RightBracket;
        }

        private static string ToString(Symbol symbol)
        {
            throw new NotImplementedException();
        }
    }
}