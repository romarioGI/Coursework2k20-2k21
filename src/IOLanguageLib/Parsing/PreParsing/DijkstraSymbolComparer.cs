using System;
using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.PreParsing
{
    public class DijkstraSymbolComparer : IComparer<Symbol>
    {
        public int Compare(Symbol first, Symbol second)
        {
            return first switch
            {
                Function firstFunction => second switch
                {
                    Predicate or PropositionalConnective or Quantifier => -1,
                    Function secondFunction => Compare(firstFunction, secondFunction),
                    _ => throw new NotSupportedException()
                },
                Predicate => second switch
                {
                    PropositionalConnective or Quantifier => -1,
                    Predicate => 0,
                    Function => 1,
                    _ => throw new NotSupportedException()
                },
                PropositionalConnective firstConnective => second switch
                {
                    Quantifier => -1,
                    PropositionalConnective secondConnective => Compare(firstConnective, secondConnective),
                    Function or Predicate => 1,
                    _ => throw new NotSupportedException()
                },
                Quantifier => second switch
                {
                    Quantifier => 0,
                    Function or Predicate or PropositionalConnective => 1,
                    _ => throw new NotSupportedException()
                },
                _ => throw new NotSupportedException()
            };
        }

        private static int Compare(Function first, Function second)
        {
            var res = GetPriority(first).CompareTo(GetPriority(second));

            return res;
        }

        private static byte GetPriority(Function connective)
        {
            return connective switch
            {
                UnaryMinus => 0,
                Exponentiation => 1,
                Multiplication or Division => 2,
                Addition or Subtraction => 3,
                _ => throw new NotSupportedException()
            };
        }

        private static int Compare(PropositionalConnective first, PropositionalConnective second)
        {
            return GetPriority(first).CompareTo(GetPriority(second));
        }

        private static byte GetPriority(PropositionalConnective connective)
        {
            return connective switch
            {
                Negation => 0,
                Conjunction => 1,
                Disjunction => 2,
                Implication => 3,
                _ => throw new NotSupportedException()
            };
        }
    }
}