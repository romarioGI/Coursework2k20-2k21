using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;

namespace IOLanguageLib.Output
{
    //TODO tests maybe integration
    public class ConverterToSymbols : IConverter<Formula, IEnumerable<Symbol>>,
        IConverter<ITerm, IEnumerable<Symbol>>, IConverter<IWord, IEnumerable<Symbol>>
    {
        private static readonly Comma Comma = new();
        private static readonly LeftBracket LeftBracket = new();
        private static readonly RightBracket RightBracket = new();
        private static readonly Space Space = new();

        public IEnumerable<Symbol> Convert(Formula formula)
        {
            var res = formula switch
            {
                QuantifierFormula quantifierFormula => OperatorAndOperandsToSymbols(
                    quantifierFormula.Quantifier,
                    quantifierFormula.ObjectVariable, quantifierFormula.SubFormula),
                PropositionalConnectiveFormula propositionalConnectiveFormula => OperatorAndOperandsToSymbols(
                    propositionalConnectiveFormula.Connective,
                    propositionalConnectiveFormula.SubFormulas.ToArray<IWord>()),
                PredicateFormula predicateFormula => OperatorAndOperandsToSymbols(
                    predicateFormula.Predicate,
                    predicateFormula.Terms.ToArray<IWord>()),
                _ => throw new NotSupportedException()
            };

            return res.Prepend(LeftBracket).Append(RightBracket);
        }

        public IEnumerable<Symbol> Convert(ITerm term)
        {
            return term switch
            {
                FunctionTerm functionTerm => OperatorAndOperandsToSymbols(functionTerm.Function,
                        functionTerm.Terms.ToArray<IWord>())
                    .Append(LeftBracket)
                    .Prepend(RightBracket),
                IndividualConstant individualConstant => individualConstant.Yield(),
                ObjectVariable objectVariable => objectVariable.Yield(),
                _ => throw new NotSupportedException()
            };
        }

        public IEnumerable<Symbol> Convert(IWord word)
        {
            return word switch
            {
                Formula formula => Convert(formula),
                ITerm term => Convert(term),
                _ => throw new NotSupportedException()
            };
        }

        private IEnumerable<Symbol> OperatorAndOperandsToSymbols<T>(T @operator,
            params IWord[] operands)
            where T : Symbol, IOperator
        {
            if (@operator.Arity != operands.Length)
                throw new ArgumentException("Count of operands should be equal operator arity.");

            var operandsSymbols = operands
                .Select(Convert)
                .ToArray();

            return @operator.Notation switch
            {
                Notation.Infix => ToSymbolsInfixOperator(@operator, operandsSymbols),
                Notation.Prefix => ToSymbolsPrefixOperator(@operator, operandsSymbols),
                Notation.Postfix => ToSymbolsPostfixOperator(@operator, operandsSymbols),
                Notation.Function => ToSymbolsFunctionOperator(@operator, operandsSymbols),
                _ => throw new NotSupportedException()
            };
        }

        private static IEnumerable<Symbol> ToSymbolsInfixOperator<T>(T @operator,
            IReadOnlyList<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            if (@operator.Arity == 2)
                return operands[0]
                    .Append(Space)
                    .Append(@operator)
                    .Append(Space)
                    .Concat(operands[1]);

            throw new NotSupportedException();
        }

        private static IEnumerable<Symbol> ToSymbolsPrefixOperator<T>(T @operator,
            IEnumerable<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            return operands.SelectMany(s => s)
                .Prepend(@operator);
        }

        private static IEnumerable<Symbol> ToSymbolsPostfixOperator<T>(T @operator,
            IEnumerable<IEnumerable<Symbol>> operands)
            where T : Symbol, IOperator
        {
            return operands.SelectMany(s => s)
                .Append(@operator);
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