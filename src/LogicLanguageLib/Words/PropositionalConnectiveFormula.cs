using System;
using System.Collections.Generic;
using System.Linq;
using LogicLanguageLib.Alphabet;

namespace LogicLanguageLib.Words
{
    public class PropositionalConnectiveFormula : Formula
    {
        private readonly Formula[] _formulas;
        public readonly IPropositionalConnective Connective;

        public PropositionalConnectiveFormula(IPropositionalConnective connective, params Formula[] formulas)
        {
            Connective = connective ?? throw new ArgumentNullException(nameof(connective));

            if (formulas is null)
                throw new ArgumentNullException(nameof(formulas));
            if (formulas.Length != Connective.Arity)
                throw new ArgumentException("count of formulas must be equal arity of connective");
            if (formulas.Any(t => t is null))
                throw new ArithmeticException("all formulas should be not null");

            _formulas = formulas;
        }

        public override IEnumerable<Formula> SubFormulas
        {
            get
            {
                foreach (var formula in _formulas)
                    yield return formula;
            }
        }

        //TODO test
        public override IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { return _formulas.SelectMany(f => f.FreeObjectVariables).Distinct(); }
        }

        //TODO test
        public override string ToString()
        {
            return $"{Connective}({string.Join<Formula>(",", _formulas)})";
        }
    }
}