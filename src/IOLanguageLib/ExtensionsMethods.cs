using System;
using System.Collections.Generic;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;

namespace IOLanguageLib
{
    internal static class ExtensionsMethods
    {
        private static readonly Comma Comma = new();
        private static readonly LeftBracket LeftBracket = new();
        private static readonly RightBracket RightBracket = new();
        private static readonly Space Space = new();

        public static IEnumerable<T> Finally<T>(this IEnumerable<T> source, Action action)
        {
            foreach (var s in source)
                yield return s;

            action();
        }

        public static IEnumerable<Symbol> ToSymbols<T, TW>(this T @operator, TW[] operands)
            where T : Symbol, IOperator
            where TW : IWord
        {
            if (@operator.Arity != operands.Length)
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

        private static IEnumerable<Symbol> ToSymbolsInfixOperator<T, TW>(T @operator, TW[] operands)
            where T : Symbol, IOperator
            where TW : IWord
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

        private static IEnumerable<Symbol> ToSymbolsPrefixOperator<T, TW>(T @operator, IEnumerable<TW> operands)
            where T : Symbol, IOperator
            where TW : IWord
        {
            yield return @operator;
            foreach (var operand in operands)
            foreach (var symbol in operand)
                yield return symbol;
        }

        private static IEnumerable<Symbol> ToSymbolsPostfixOperator<T, TW>(T @operator, IEnumerable<TW> operands)
            where T : Symbol, IOperator
            where TW : IWord
        {
            foreach (var operand in operands)
            foreach (var symbol in operand)
                yield return symbol;
            yield return @operator;
        }

        private static IEnumerable<Symbol> ToSymbolsFunctionOperator<T, TW>(T @operator, IReadOnlyList<TW> operands)
            where T : Symbol, IOperator
            where TW : IWord
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