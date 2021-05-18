using System;

namespace IOLib
{
    public class Symbol : IEquatable<Symbol>
    {
        private readonly string _string;

        public Symbol(string s)
        {
            _string = s ?? throw new NullReferenceException();
        }

        public bool Equals(Symbol other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _string == other._string;
        }

        public override string ToString()
        {
            return _string;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Symbol) obj);
        }

        public override int GetHashCode()
        {
            return _string != null ? _string.GetHashCode() : 0;
        }
    }
}