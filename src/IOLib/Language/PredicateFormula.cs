using System;
using System.Collections.Generic;
using System.Linq;

namespace IOLib.Language
{
    public class PredicateFormula : Formula
    {
        private static readonly IReadOnlySet<Symbol> Predicates = new HashSet<Symbol>
        {
            Alphabet.Less, Alphabet.More, Alphabet.Equal
        };

        public readonly Symbol Predicate;
        public readonly Term[] Terms;

        public PredicateFormula(Symbol symbol, params Term[] terms)
        {
            Predicate = symbol ?? throw new ArgumentNullException(nameof(symbol));

            if (!IsPredicate(symbol))
                throw new ArgumentException("Symbol should be predicate.");

            if (terms is null)
                throw new ArgumentNullException(nameof(terms));
            if (terms.Any(t => t is null))
                throw new ArithmeticException("All terms should be not null.");

            Terms = terms;
        }

        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return Terms.SelectMany(t => t.ObjectVariables).Distinct(); }
        }

        public override string ToString()
        {
            return $"{Predicate}({string.Join<Term>(",", Terms)})";
        }

        private static bool IsPredicate(Symbol symbol)
        {
            return Predicates.Contains(symbol);
        }
    }
}