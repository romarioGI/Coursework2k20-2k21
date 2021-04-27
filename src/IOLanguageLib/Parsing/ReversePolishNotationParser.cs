using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Exceptions;
using IOLanguageLib.Parsing.Contexts;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Parsing
{
    //TODO tests
    public class ReversePolishNotationParser : FormulaParser
    {
        public override Formula Parse(IEnumerable<Symbol> input)
        {
            var context = new ReversePolishNotationParserContext(input);

            return Parse(context);
        }

        private static Formula Parse(ReversePolishNotationParserContext context)
        {
            while (context.MoveNext())
                ProcessSymbol(context);

            return GetResultFormula(context);
        }

        private static void ProcessSymbol(ReversePolishNotationParserContext context)
        {
            var word = context.CurrentSymbol switch
            {
                IOperator @operator => CalcWord(context, @operator),
                ITerm term => term,
                _ => throw new UnexpectedSymbol(context.Index)
            };

            context.PushWord(word);
        }

        private static IWord CalcWord(ReversePolishNotationParserContext context, IOperator @operator)
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

        private static IEnumerable<IWord> GetOperands(ReversePolishNotationParserContext context, byte count)
        {
            if (context.Count < count)
                throw new IndexedInputException(context.Index,
                    $"Not enough operands. There should be {count} operands, but there are only {context.Count}.");

            return context.PopWords(count).Reverse();
        }

        private static ITerm[] GetTerms(ReversePolishNotationParserContext context, byte count)
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

        private static ObjectVariable GetObjectVariable(ReversePolishNotationParserContext context)
        {
            var word = GetOperands(context, 1).First();
            if (word is ObjectVariable variable)
                return variable;

            throw new UnexpectedSymbol(context.Index, "Expected object variable.");
        }

        private static Formula[] GetFormulas(ReversePolishNotationParserContext context, byte count)
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

        private static Formula GetFormula(ReversePolishNotationParserContext context)
        {
            return GetFormulas(context, 1)[0];
        }

        private static FunctionTerm CalcWord(ReversePolishNotationParserContext context, Function function)
        {
            var terms = GetTerms(context, function.Arity);
            return new FunctionTerm(function, terms);
        }

        private static PredicateFormula CalcWord(ReversePolishNotationParserContext context, Predicate predicate)
        {
            var terms = GetTerms(context, predicate.Arity);
            return new PredicateFormula(predicate, terms);
        }

        private static QuantifierFormula CalcWord(ReversePolishNotationParserContext context, Quantifier quantifier)
        {
            var formula = GetFormula(context);
            var objectVariable = GetObjectVariable(context);
            return new QuantifierFormula(quantifier, objectVariable, formula);
        }

        private static PropositionalConnectiveFormula CalcWord(ReversePolishNotationParserContext context,
            PropositionalConnective connective)
        {
            var formulas = GetFormulas(context, connective.Arity);
            return new PropositionalConnectiveFormula(connective, formulas);
        }

        private static Formula GetResultFormula(ReversePolishNotationParserContext context)
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