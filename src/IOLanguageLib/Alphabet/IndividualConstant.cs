using System.Collections.Generic;
using System.Numerics;
using IOLanguageLib.Words;

namespace IOLanguageLib.Alphabet
{
    public class IndividualConstant : Symbol, ITerm
    {
        public readonly BigInteger Value;

        private IndividualConstant(BigInteger value)
        {
            Value = value;
        }

        public IndividualConstant(int value)
        {
            Value = value;
        }

        protected override string DefaultRepresentation => $"c_{Value}";

        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { yield break; }
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            yield return this;
        }

        public static implicit operator BigInteger(IndividualConstant constant)
        {
            return constant.Value;
        }

        public static implicit operator IndividualConstant(BigInteger value)
        {
            return new(value);
        }

        public static implicit operator IndividualConstant(int value)
        {
            return new(value);
        }
    }
}