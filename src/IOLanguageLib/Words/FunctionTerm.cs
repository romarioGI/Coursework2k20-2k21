using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public class FunctionTerm : ITerm
    {
        public readonly Function Function;
        public readonly ITerm[] Terms;

        public FunctionTerm(Function function, params ITerm[] terms)
        {
            Function = function ?? throw new ArgumentNullException(nameof(function));

            if (terms is null)
                throw new ArgumentNullException(nameof(terms));
            if (terms.Length != Function.Arity)
                throw new ArgumentException("count of terms must be equal arity of function");
            if (terms.Any(t => t is null))
                throw new ArithmeticException("all terms should be not null");

            Terms = terms;
        }

        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return Terms.SelectMany(t => t.FreeObjectVariables).Distinct(); }
        }

        public override string ToString()
        {
            return $"{Function}({string.Join<ITerm>(",", Terms)})";
        }
    }
}