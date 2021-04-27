using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public class QuantifierFormula : Formula
    {
        private readonly ObjectVariable _objectVariable;
        private readonly Quantifier _quantifier;
        private readonly Formula _subFormula;

        public QuantifierFormula(Quantifier quantifier, ObjectVariable objectVariable, Formula formula)
        {
            _quantifier = quantifier ?? throw new ArgumentNullException(nameof(quantifier));
            _objectVariable = objectVariable ?? throw new ArgumentNullException(nameof(objectVariable));
            _subFormula = formula ?? throw new ArgumentNullException(nameof(formula));
        }

        public override IEnumerable<Formula> SubFormulas
        {
            get { yield return _subFormula; }
        }

        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return _subFormula.FreeObjectVariables.Where(o => !o.Equals(_objectVariable)); }
        }

        public override IEnumerator<Symbol> GetEnumerator()
        {
            yield return _quantifier;
            yield return _objectVariable;
            yield return new LeftBracket();
            foreach (var symbol in _subFormula) yield return symbol;
            yield return new RightBracket();
        }

        public override string ToString()
        {
            return $"({_quantifier}{_objectVariable}){_subFormula}";
        }
    }
}