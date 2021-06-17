using System.Collections.Generic;

namespace IOLib.Language
{
    public abstract class Term
    {
        public abstract IEnumerable<ObjectVariable> ObjectVariables { get; }

        public abstract override string ToString();
    }
}