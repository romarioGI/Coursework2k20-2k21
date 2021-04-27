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

            switch (@operator.Notation)
            {
                case Notation.Infix when @operator.Arity == 2:
                {
                    foreach (var symbol in operands[0])
                        yield return symbol;
                    yield return Space;
                    yield return @operator;
                    yield return Space;
                    foreach (var symbol in operands[1])
                        yield return symbol;
                    break;
                }
                case Notation.Infix:
                    throw new NotSupportedException();
                case Notation.Prefix:
                {
                    yield return @operator;
                    foreach (var operand in operands)
                    foreach (var symbol in operand)
                        yield return symbol;
                    break;
                }
                case Notation.Postfix:
                {
                    foreach (var operand in operands)
                    foreach (var symbol in operand)
                        yield return symbol;
                    yield return @operator;
                    break;
                }
                case Notation.Function:
                {
                    yield return @operator;
                    yield return LeftBracket;

                    if (@operator.Arity != 0)
                    {
                        foreach (var symbol in operands[0])
                            yield return symbol;
                        for (var i = 1; i < operands.Length; i++)
                        {
                            yield return Comma;
                            yield return Space;
                            foreach (var symbol in operands[i])
                                yield return symbol;
                        }
                    }

                    yield return RightBracket;
                    break;
                }
                default:
                    throw new NotSupportedException();
            }
        }
    }
}