﻿using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Exceptions;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Parsing
{
    //TODO tests
    public class ReversePolishNotationParser : IParser
    {
        private int _index;
        private Stack<IWord> _stack;

        public Formula Parse(IEnumerable<Symbol> symbols)
        {
            _stack = new Stack<IWord>();
            _index = 0;

            foreach (var symbol in symbols)
            {
                ProcessSymbol(symbol);
                _index++;
            }

            return GetResultFormula();
        }

        private void ProcessSymbol(Symbol symbol)
        {
            var word = symbol switch
            {
                IOperator @operator => CalcWord(@operator),
                ITerm term => term,
                _ => throw new UnexpectedCharacter(_index)
            };

            _stack.Push(word);
        }

        private IWord CalcWord(IOperator @operator)
        {
            return @operator switch
            {
                Function function => CalcWord(function),
                Predicate predicate => CalcWord(predicate),
                IQuantifier quantifier => CalcWord(quantifier),
                IPropositionalConnective connective => CalcWord(connective),
                _ => throw new NotSupportedException()
            };
        }

        private IEnumerable<IWord> GetOperands(int count)
        {
            if (_stack.Count < count)
                throw new IndexedInputException(_index,
                    $"Not enough operands. There should be {count} operands, but there are only {_stack.Count}.");

            while (count-- > 0)
                yield return _stack.Pop();
        }

        private ITerm[] GetTerms(int count)
        {
            return GetOperands(count)
                .Select(ToTerm)
                .ToArray();
        }

        private ITerm ToTerm(IWord word, int wordNumber)
        {
            throw new NotImplementedException();
        }

        private static ObjectVariable GetObjectVariable()
        {
            throw new NotImplementedException();
        }

        private Formula[] GetFormulas(int count)
        {
            return GetOperands(count)
                .Select(ToFormula)
                .ToArray();
        }

        private Formula ToFormula(IWord word, int wordNumber)
        {
            throw new NotImplementedException();
        }

        private Formula GetFormula()
        {
            return GetFormulas(1)[0];
        }

        private FunctionTerm CalcWord(Function function)
        {
            var terms = GetTerms(function.Arity);
            return new FunctionTerm(function, terms);
        }

        private PredicateFormula CalcWord(Predicate predicate)
        {
            var terms = GetTerms(predicate.Arity);
            return new PredicateFormula(predicate, terms);
        }

        private QuantifierFormula CalcWord(IQuantifier quantifier)
        {
            var objectVariable = GetObjectVariable();
            var formula = GetFormula();
            return new QuantifierFormula(quantifier, objectVariable, formula);
        }

        private PropositionalConnectiveFormula CalcWord(IPropositionalConnective connective)
        {
            var formulas = GetFormulas(connective.Arity);
            return new PropositionalConnectiveFormula(connective, formulas);
        }

        private Formula GetResultFormula()
        {
            //TODO обработка ошибок
            if (_stack.Count == 0)
                throw new Exception("Empty stack.");

            if (_stack.Count > 1)
                throw new Exception("Stack contains more then one element.");

            return (Formula) _stack.Peek();
        }
    }
}