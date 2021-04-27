using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public class FunctionTerm : ITerm
    {
        private readonly Function _function;
        private readonly ITerm[] _terms;

        public FunctionTerm(Function function, params ITerm[] terms)
        {
            _function = function ?? throw new ArgumentNullException(nameof(function));

            if (terms is null)
                throw new ArgumentNullException(nameof(terms));
            if (terms.Length != _function.Arity)
                throw new ArgumentException("count of terms must be equal arity of function");
            if (terms.Any(t => t is null))
                throw new ArithmeticException("all terms should be not null");

            _terms = terms;
        }

        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return _terms.SelectMany(t => t.FreeObjectVariables).Distinct(); }
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            return _function.ToSymbols(_terms).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"{_function}({string.Join<ITerm>(",", _terms)})";
        }
    }
}