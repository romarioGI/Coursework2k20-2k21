using System;
using System.Collections.Generic;
using LogicLanguageLib.Words;

namespace LogicLanguageLib.Alphabet
{
    public sealed class ObjectVariable : Symbol, ITerm, IEquatable<ObjectVariable>
    {
        public readonly char Char;
        public readonly uint? Index;

        public ObjectVariable(char c, uint? i = null)
        {
            if (!char.IsLetter(c))
                throw new ArgumentException("c should be letter");

            Char = c;
            Index = i;
        }

        protected override string DefaultRepresentation => $"{Char}{(Index is null ? "" : $"_{Index}")}";

        public bool Equals(ObjectVariable other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Char == other.Char && Index == other.Index;
        }

        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { yield return this; }
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is ObjectVariable other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Char, Index);
        }
    }
}