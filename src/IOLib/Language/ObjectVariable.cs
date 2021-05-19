using System;
using System.Collections.Generic;

namespace IOLib.Language
{
    public class ObjectVariable : Term, IEquatable<ObjectVariable>
    {
        public readonly IndividualConstant Index;
        public readonly Symbol Letter;

        public ObjectVariable(Symbol letter, IndividualConstant constant = null)
        {
            if (!Alphabet.IsLetter(letter))
                throw new ArgumentException("Symbol must be letter.");

            Letter = letter;
            Index = constant;
        }

        public override IEnumerable<ObjectVariable> ObjectVariables
        {
            get { yield return this; }
        }

        public bool Equals(ObjectVariable other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index.Equals(other.Index) && Letter.Equals(other.Letter);
        }


        public override string ToString()
        {
            return $"{Letter}{(Index is null ? "" : $"_{Index}")}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ObjectVariable) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Letter);
        }
    }
}