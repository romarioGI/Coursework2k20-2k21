using System;
using System.Collections.Generic;
using System.Linq;
using LogicLanguageLib.Alphabet;

namespace LogicLanguageLib.Words
{
    public class FunctionTerm : ITerm
    {
        private readonly ITerm[] _terms;
        public readonly Function Function;

        public FunctionTerm(Function function, params ITerm[] terms)
        {
            Function = function ?? throw new ArgumentNullException(nameof(function));

            if (terms is null)
                throw new ArgumentNullException(nameof(terms));
            if (terms.Length != Function.Arity)
                throw new ArgumentException("count of terms must be equal arity of function");
            if (terms.Any(t => t is null))
                throw new ArithmeticException("all terms should be not null");

            _terms = terms;
        }

        public IEnumerable<ITerm> Terms
        {
            get
            {
                foreach (var term in _terms)
                    yield return term;
            }
        }

        //TODO test
        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return _terms.SelectMany(t => t.FreeObjectVariables).Distinct(); }
        }

        //TODO test
        public override string ToString()
        {
            return $"{Function}({string.Join<ITerm>(",", _terms)})";
        }
    }
}