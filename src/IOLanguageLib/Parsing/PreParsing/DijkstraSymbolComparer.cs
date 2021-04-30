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
                Predicate firstPredicate => second switch
                {
                    PropositionalConnective or Quantifier => -1,
                    Predicate secondPredicate => Compare(firstPredicate, secondPredicate),
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
                Quantifier firstQuantifier => second switch
                {
                    Quantifier secondQuantifier => Compare(firstQuantifier, secondQuantifier),
                    Function or Predicate or PropositionalConnective => 1,
                    _ => throw new NotSupportedException()
                },
                _ => throw new NotSupportedException()
            };
        }

        private static int Compare(Function first, Function second)
        {
            var res = GetPriority(first).CompareTo(GetPriority(second));

            if (res == 0)
                res = Compare((IOperator) first, second);

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

        private static int Compare(Predicate first, Predicate second)
        {
            return Compare((IOperator) first, second);
        }

        private static int Compare(PropositionalConnective first, PropositionalConnective second)
        {
            var res = GetPriority(first).CompareTo(GetPriority(second));

            if (res == 0)
                res = Compare((IOperator) first, second);

            return res;
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

        private static int Compare(Quantifier first, Quantifier second)
        {
            return Compare((IOperator) first, second);
        }

        private static int Compare(IOperator first, IOperator second)
        {
            return Compare(first.Associativity, second.Associativity);
        }

        private static int Compare(Associativity first, Associativity second)
        {
            if (first == Associativity.Left)
                return -1;

            if (second == Associativity.Right)
                return 1;

            return 0;
        }
    }
}