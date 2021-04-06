using System;

namespace LogicLanguageLib.Alphabet
{
    public abstract class Symbol : IEquatable<Symbol>
    {
        protected abstract string DefaultRepresentation { get; }

        public bool Equals(Symbol other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DefaultRepresentation == other.DefaultRepresentation;
        }

        public override string ToString()
        {
            return DefaultRepresentation;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Symbol) obj);
        }

        public override int GetHashCode()
        {
            return DefaultRepresentation != null ? DefaultRepresentation.GetHashCode() : 0;
        }
    }
}