using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public class PredicateFormula : Formula
    {
        public readonly Predicate Predicate;
        public readonly ITerm[] Terms;

        public PredicateFormula(Predicate predicate, params ITerm[] terms)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

            if (terms is null)
                throw new ArgumentNullException(nameof(terms));
            if (terms.Length != Predicate.Arity)
                throw new ArgumentException("count of formulas must be equal arity of predicate");
            if (terms.Any(t => t is null))
                throw new ArithmeticException("all terms should be not null");

            Terms = terms;
        }

        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return Terms.SelectMany(t => t.FreeObjectVariables).Distinct(); }
        }

        public override string ToString()
        {
            return
                $"{Predicate}({string.Join<ITerm>(",", Terms)})";
        }
    }
}