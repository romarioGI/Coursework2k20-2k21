using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public class PropositionalConnectiveFormula : Formula
    {
        public readonly PropositionalConnective Connective;
        public readonly Formula[] SubFormulas;

        public PropositionalConnectiveFormula(PropositionalConnective connective, params Formula[] subFormulas)
        {
            Connective = connective ?? throw new ArgumentNullException(nameof(connective));

            if (subFormulas is null)
                throw new ArgumentNullException(nameof(subFormulas));
            if (subFormulas.Length != Connective.Arity)
                throw new ArgumentException("count of formulas must be equal arity of connective");
            if (subFormulas.Any(t => t is null))
                throw new ArithmeticException("all formulas should be not null");

            SubFormulas = subFormulas;
        }

        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return SubFormulas.SelectMany(f => f.FreeObjectVariables).Distinct(); }
        }

        public override string ToString()
        {
            return $"{Connective}({string.Join<Formula>(",", SubFormulas)})";
        }
    }
}