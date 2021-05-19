using System.Collections.Generic;
using System.Numerics;

namespace IOLib.Language
{
    public sealed class IndividualConstant : Term
    {
        public readonly BigInteger Value;

        public IndividualConstant(BigInteger value)
        {
            Value = value;
        }

        public IndividualConstant(int value)
        {
            Value = value;
        }

        public override IEnumerable<ObjectVariable> ObjectVariables
        {
            get { yield break; }
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

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}