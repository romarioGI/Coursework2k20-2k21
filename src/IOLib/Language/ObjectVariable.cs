using System;
using System.Collections.Generic;

namespace IOLib.Language
{
    public class ObjectVariable : Term, IEquatable<ObjectVariable>
    {
        public readonly char Char;
        public readonly uint? Index;

        public ObjectVariable(char c, uint? i = null)
        {
            if (!char.IsLetter(c))
                throw new ArgumentException("Argument c should be letter.");

            Char = c;
            Index = i;
        }

        public override IEnumerable<ObjectVariable> ObjectVariables
        {
            get { yield return this; }
        }

        public bool Equals(ObjectVariable other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Char == other.Char && Index == other.Index;
        }

        public override string ToString()
        {
            return $"{Char}{(Index is null ? "" : $"_{Index}")}";
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
            return HashCode.Combine(Char, Index);
        }
    }
}