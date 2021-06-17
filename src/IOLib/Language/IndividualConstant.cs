using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace IOLib.Language
{
    //TODO протестировать Equal, для переменных тоже
    public class IndividualConstant : Term, IEquatable<IndividualConstant>
    {
        private readonly IndividualConstant _constant;
        private readonly Symbol _digit;

        public IndividualConstant(Symbol digit, IndividualConstant constant = null)
        {
            if (!Alphabet.IsDigit(digit))
                throw new ArgumentException("Symbol must be digit.");

            _digit = digit;
            _constant = constant;
        }

        public override IEnumerable<ObjectVariable> ObjectVariables
        {
            get { yield break; }
        }

        private IEnumerable<Symbol> Digits => _constant is not null ? _constant.Digits.Append(_digit) : _digit.Yield();

        public bool Equals(IndividualConstant other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _digit.Equals(other._digit) && _constant.Equals(other._constant);
        }

        public BigInteger ToBigInteger()
        {
            var res = new BigInteger();
            var ten = new BigInteger(10);

            return Digits.Aggregate(res, (current, digit) => current * ten + BigInteger.Parse(digit.ToString()));
        }

        public override string ToString()
        {
            return _constant is null ? _digit.ToString() : $"{_digit}{_constant}";
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is IndividualConstant other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_digit, _constant);
        }
    }
}