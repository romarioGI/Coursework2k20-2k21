using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public class PredicateFormula : Formula
    {
        private readonly ITerm[] _terms;
        public readonly Predicate Predicate;

        public PredicateFormula(Predicate predicate, params ITerm[] terms)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

            if (terms is null)
                throw new ArgumentNullException(nameof(terms));
            if (terms.Length != Predicate.Arity)
                throw new ArgumentException("count of formulas must be equal arity of predicate");
            if (terms.Any(t => t is null))
                throw new ArithmeticException("all terms should be not null");

            _terms = terms;
        }

        public IEnumerable<ITerm> Terms => _terms;

        public override IEnumerable<Formula> SubFormulas
        {
            get { yield break; }
        }

        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return _terms.SelectMany(t => t.FreeObjectVariables).Distinct(); }
        }

        public override string ToString()
        {
            return
                $"{Predicate}({string.Join<ITerm>(",", _terms)})";
        }
    }
}