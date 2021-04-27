using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public class PropositionalConnectiveFormula : Formula
    {
        private readonly Formula[] _formulas;
        private readonly PropositionalConnective _connective;

        public PropositionalConnectiveFormula(PropositionalConnective connective, params Formula[] formulas)
        {
            _connective = connective ?? throw new ArgumentNullException(nameof(connective));

            if (formulas is null)
                throw new ArgumentNullException(nameof(formulas));
            if (formulas.Length != _connective.Arity)
                throw new ArgumentException("count of formulas must be equal arity of connective");
            if (formulas.Any(t => t is null))
                throw new ArithmeticException("all formulas should be not null");

            _formulas = formulas;
        }

        public override IEnumerable<Formula> SubFormulas => _formulas;

        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return _formulas.SelectMany(f => f.FreeObjectVariables).Distinct(); }
        }

        public override IEnumerator<Symbol> GetEnumerator()
        {
            return _connective
                .ToSymbols(_formulas)
                .GetEnumerator();
        }

        public override string ToString()
        {
            return $"{_connective}({string.Join<Formula>(",", _formulas)})";
        }
    }
}