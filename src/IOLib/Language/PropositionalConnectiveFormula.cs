using System;
using System.Collections.Generic;
using System.Linq;

namespace IOLib.Language
{
    public class PropositionalConnectiveFormula : Formula
    {
        private static readonly IReadOnlySet<Symbol> Connectives = new HashSet<Symbol>
        {
            Alphabet.Conjunction, Alphabet.Disjunction, Alphabet.Implication, Alphabet.Negation
        };

        public readonly Symbol Connective;
        public readonly Formula[] SubFormulas;

        public PropositionalConnectiveFormula(Symbol symbol, params Formula[] subFormulas)
        {
            Connective = symbol ?? throw new ArgumentNullException(nameof(symbol));

            if (!IsConnective(symbol))
                throw new ArgumentException("Symbol should be connective.");

            if (subFormulas is null)
                throw new ArgumentNullException(nameof(subFormulas));
            if (subFormulas.Any(t => t is null))
                throw new ArithmeticException("All formulas should be not null.");

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

        private static bool IsConnective(Symbol symbol)
        {
            return Connectives.Contains(symbol);
        }
    }
}