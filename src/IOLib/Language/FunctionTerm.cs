using System;
using System.Collections.Generic;
using System.Linq;

namespace IOLib.Language
{
    public class FunctionTerm : Term
    {
        private static readonly IReadOnlySet<Symbol> Functions = new HashSet<Symbol>
        {
            Alphabet.Plus, Alphabet.Minus, Alphabet.Multiplication, Alphabet.Division, Alphabet.Exponentiation
        };

        public readonly Symbol Function;
        public readonly Term[] Terms;

        public FunctionTerm(Symbol symbol, params Term[] terms)
        {
            Function = symbol ?? throw new ArgumentNullException(nameof(symbol));

            if (!IsFunction(symbol))
                throw new ArgumentException("Symbol should be function.");

            if (terms is null)
                throw new ArgumentNullException(nameof(terms));
            if (terms.Any(t => t is null))
                throw new ArithmeticException("All terms should be not null.");

            Terms = terms;
        }

        public override IEnumerable<ObjectVariable> ObjectVariables
        {
            get { return Terms.SelectMany(t => t.ObjectVariables).Distinct(); }
        }

        public override string ToString()
        {
            return $"{Function}({string.Join<Term>(",", Terms)})";
        }

        private static bool IsFunction(Symbol symbol)
        {
            return Functions.Contains(symbol);
        }
    }
}