using System;
using System.Collections.Generic;
using System.Linq;

namespace IOLib.Language
{
    public class QuantifierFormula : Formula
    {
        private static readonly IReadOnlySet<Symbol> Quantifiers = new HashSet<Symbol>
        {
            Alphabet.ExistentialQuantifier, Alphabet.UniversalQuantifier
        };

        public readonly ObjectVariable ObjectVariable;
        public readonly Symbol Quantifier;
        public readonly Formula SubFormula;

        public QuantifierFormula(Symbol symbol, ObjectVariable objectVariable, Formula formula)
        {
            Quantifier = symbol ?? throw new ArgumentNullException(nameof(symbol));
            ObjectVariable = objectVariable ?? throw new ArgumentNullException(nameof(objectVariable));
            SubFormula = formula ?? throw new ArgumentNullException(nameof(formula));

            if (!IsQuantifier(symbol))
                throw new ArgumentException("Symbol should be quantifier.");
        }

        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return SubFormula.FreeObjectVariables.Where(o => !o.Equals(ObjectVariable)); }
        }

        public override string ToString()
        {
            return $"{Quantifier}{ObjectVariable}{SubFormula}";
        }

        private static bool IsQuantifier(Symbol symbol)
        {
            return Quantifiers.Contains(symbol);
        }
    }
}