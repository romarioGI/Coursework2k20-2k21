using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;

namespace IOLanguageLib.Output
{
    public class FormulaConverterToSymbols : IConverter<Formula, IEnumerable<Symbol>>,
        IConverter<ITerm, IEnumerable<Symbol>>
    {
        private static readonly Comma Comma = new();
        private static readonly LeftBracket LeftBracket = new();
        private static readonly RightBracket RightBracket = new();
        private static readonly Space Space = new();

        public IEnumerable<Symbol> Convert(Formula formula)
        {
            yield return LeftBracket;

            switch (formula)
            {
                case QuantifierFormula quantifierFormula:
                {
                    yield return quantifierFormula.Quantifier;
                    yield return quantifierFormula.ObjectVariable;

                    var subFormulaSymbols = Convert(quantifierFormula.SubFormula);
                    foreach (var symbol in subFormulaSymbols) yield return symbol;
                    break;
                }
                case PropositionalConnectiveFormula propositionalConnectiveFormula:
                {
                    var symbolsOfSubFormulas = propositionalConnectiveFormula.SubFormulas.Select(Convert).ToArray();
                    var symbols = OperatorAndOperandsToSymbols(propositionalConnectiveFormula.Connective,
                        symbolsOfSubFormulas);

                    foreach (var symbol in symbols)
                        yield return symbol;
                    break;
                }
                case PredicateFormula predicateFormula:
                {
                    var symbolsOfTerms = predicateFormula.Terms.Select(Convert).ToArray();
                    var symbols = OperatorAndOperandsToSymbols(predicateFormula.Predicate, symbolsOfTerms);

                    foreach (var symbol in symbols)
                        yield return symbol;
                    break;
                }
                default:
                    throw new NotSupportedException();
            }

            yield return RightBracket;
        }

        public IEnumerable<Symbol> Convert(ITerm term)
        {
            switch (term)
            {
                case FunctionTerm functionTerm:
                {
                    yield return new LeftBracket();

                    var symbolsOfTerms = functionTerm.Terms.Select(Convert).ToArray();
                    var symbols = OperatorAndOperandsToSymbols(functionTerm.Function, symbolsOfTerms);

                    foreach (var symbol in symbols)
                        yield return symbol;

                    yield return new RightBracket();
                    break;
                }
                case IndividualConstant individualConstant:
                    yield return individualConstant;
                    break;
                case ObjectVariable objectVariable:
                    yield return objectVariable;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private static IEnumerable<Symbol> OperatorAndOperandsToSymbols<T>(T @operator,
            IReadOnlyList<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            if (@operator.Arity != operands.Count)
                throw new ArgumentException("Count of operands should be equal operator arity.");

            return @operator.Notation switch
            {
                Notation.Infix => ToSymbolsInfixOperator(@operator, operands),
                Notation.Prefix => ToSymbolsPrefixOperator(@operator, operands),
                Notation.Postfix => ToSymbolsPostfixOperator(@operator, operands),
                Notation.Function => ToSymbolsFunctionOperator(@operator, operands),
                _ => throw new NotSupportedException()
            };
        }

        private static IEnumerable<Symbol> ToSymbolsInfixOperator<T>(T @operator,
            IReadOnlyList<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            if (@operator.Arity == 2)
            {
                foreach (var symbol in operands[0])
                    yield return symbol;
                yield return Space;
                yield return @operator;
                yield return Space;
                foreach (var symbol in operands[1])
                    yield return symbol;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static IEnumerable<Symbol> ToSymbolsPrefixOperator<T>(T @operator,
            IEnumerable<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            yield return @operator;
            foreach (var operand in operands)
            foreach (var symbol in operand)
                yield return symbol;
        }

        private static IEnumerable<Symbol> ToSymbolsPostfixOperator<T>(T @operator,
            IEnumerable<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            foreach (var operand in operands)
            foreach (var symbol in operand)
                yield return symbol;
            yield return @operator;
        }

        private static IEnumerable<Symbol> ToSymbolsFunctionOperator<T>(T @operator,
            IReadOnlyList<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            yield return @operator;
            yield return LeftBracket;

            if (@operator.Arity != 0)
            {
                foreach (var symbol in operands[0])
                    yield return symbol;
                for (var i = 1; i < operands.Count; i++)
                {
                    yield return Comma;
                    yield return Space;
                    foreach (var symbol in operands[i])
                        yield return symbol;
                }
            }

            yield return RightBracket;
        }
    }
}