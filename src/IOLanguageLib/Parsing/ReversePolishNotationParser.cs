using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Exceptions;
using IOLanguageLib.Parsing.Contexts;
using IOLanguageLib.Words;

namespace IOLanguageLib.Parsing
{
    //TODO tests
    public class ReversePolishNotationParser : FormulaParser
    {
        public override Formula Parse(IEnumerable<Symbol> input)
        {
            var context = new SymbolContextWithStack<IWord>(input);

            return Parse(context);
        }

        private static Formula Parse(SymbolContextWithStack<IWord> context)
        {
            while (context.MoveNext())
                ProcessSymbol(context);

            return GetResultFormula(context);
        }

        private static void ProcessSymbol(SymbolContextWithStack<IWord> context)
        {
            var word = context.CurrentSymbol switch
            {
                IOperator @operator => CalcWord(context, @operator),
                ITerm term => term,
                _ => throw new UnexpectedSymbol(context.Index)
            };

            context.Push(word);
        }

        private static IWord CalcWord(SymbolContextWithStack<IWord> context, IOperator @operator)
        {
            return @operator switch
            {
                Function function => CalcWord(context, function),
                Predicate predicate => CalcWord(context, predicate),
                Quantifier quantifier => CalcWord(context, quantifier),
                PropositionalConnective connective => CalcWord(context, connective),
                _ => throw new NotSupportedException()
            };
        }

        private static IEnumerable<IWord> GetOperands(SymbolContextWithStack<IWord> context, byte count)
        {
            if (context.Count < count)
                throw new IndexedInputException(context.Index,
                    $"Not enough operands. There should be {count} operands, but there are only {context.Count}.");

            return context.PopItems(count).Reverse();
        }

        private static ITerm[] GetTerms(SymbolContextWithStack<IWord> context, byte count)
        {
            return GetOperands(context, count)
                .Select((word, wordNumber) => ToTerm(context, word, wordNumber))
                .ToArray();
        }

        private static ITerm ToTerm(SymbolContext context, IWord word, int wordNumber)
        {
            if (word is ITerm term)
                return term;

            throw new IndexedInputException(context.Index, $"Operand number {wordNumber} is not term.");
        }

        private static ObjectVariable GetObjectVariable(SymbolContextWithStack<IWord> context)
        {
            var word = GetOperands(context, 1).First();
            if (word is ObjectVariable variable)
                return variable;

            throw new UnexpectedSymbol(context.Index, "Expected object variable.");
        }

        private static Formula[] GetFormulas(SymbolContextWithStack<IWord> context, byte count)
        {
            return GetOperands(context, count)
                .Select((word, i) => ToFormula(context, word, i))
                .ToArray();
        }

        private static Formula ToFormula(SymbolContext context, IWord word, int wordNumber)
        {
            if (word is Formula formula)
                return formula;

            throw new IndexedInputException(context.Index, $"Operand number {wordNumber} is not function.");
        }

        private static Formula GetFormula(SymbolContextWithStack<IWord> context)
        {
            return GetFormulas(context, 1)[0];
        }

        private static FunctionTerm CalcWord(SymbolContextWithStack<IWord> context, Function function)
        {
            var terms = GetTerms(context, function.Arity);
            return new FunctionTerm(function, terms);
        }

        private static PredicateFormula CalcWord(SymbolContextWithStack<IWord> context, Predicate predicate)
        {
            var terms = GetTerms(context, predicate.Arity);
            return new PredicateFormula(predicate, terms);
        }

        private static QuantifierFormula CalcWord(SymbolContextWithStack<IWord> context, Quantifier quantifier)
        {
            var formula = GetFormula(context);
            var objectVariable = GetObjectVariable(context);
            return new QuantifierFormula(quantifier, objectVariable, formula);
        }

        private static PropositionalConnectiveFormula CalcWord(SymbolContextWithStack<IWord> context,
            PropositionalConnective connective)
        {
            var formulas = GetFormulas(context, connective.Arity);
            return new PropositionalConnectiveFormula(connective, formulas);
        }

        private static Formula GetResultFormula(SymbolContextWithStack<IWord> context)
        {
            switch (context.Count)
            {
                case 0:
                    throw new Exception("Empty stack.");
                case > 1:
                    throw new UnexpectedEndOfInput("Expected operator.");
            }

            var word = context.Peek;
            if (word is Formula formula)
                return formula;

            throw new UnexpectedEndOfInput($"Input word is not formula. It is {word.GetType()}");
        }
    }
}