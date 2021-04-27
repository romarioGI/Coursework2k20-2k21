using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using IOLanguageLib.Words;

namespace IOLanguageLib.Alphabet
{
    public class IndividualConstant : Symbol, ITerm
    {
        private readonly BigInteger _value;

        private IndividualConstant(BigInteger value)
        {
            _value = value;
        }

        public IndividualConstant(int value)
        {
            _value = value;
        }

        protected override string DefaultRepresentation => $"c_{_value}";

        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { yield break; }
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator BigInteger(IndividualConstant constant)
        {
            return constant._value;
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