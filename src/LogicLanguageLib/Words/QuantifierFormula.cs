using System;
using System.Collections.Generic;
using System.Linq;
using LogicLanguageLib.Alphabet;

namespace LogicLanguageLib.Words
{
    public class QuantifierFormula : Formula
    {
        public readonly ObjectVariable ObjectVariable;
        public readonly IQuantifier Quantifier;
        public readonly Formula SubFormula;

        public QuantifierFormula(IQuantifier quantifier, ObjectVariable objectVariable, Formula formula)
        {
            Quantifier = quantifier ?? throw new ArgumentNullException(nameof(quantifier));
            ObjectVariable = objectVariable ?? throw new ArgumentNullException(nameof(objectVariable));
            SubFormula = formula ?? throw new ArgumentNullException(nameof(formula));
        }

        public override IEnumerable<Formula> SubFormulas
        {
            get { yield return SubFormula; }
        }
        
        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return SubFormula.FreeObjectVariables.Where(o => !o.Equals(ObjectVariable)); }
        }

        public override string ToString()
        {
            return $"({Quantifier}{ObjectVariable}){SubFormula}";
        }
    }
}