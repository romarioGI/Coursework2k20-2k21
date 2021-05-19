using System.Collections.Generic;

namespace IOLib.Language
{
    public abstract class Formula
    {
        public abstract IEnumerable<ObjectVariable> FreeObjectVariables { get; }

        public abstract override string ToString();
    }
}