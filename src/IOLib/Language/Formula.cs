using System.Collections.Generic;

namespace IOLib.Language
{
    //TODO проверять арность внутри, чтобы не выносить логику арности наружу и для термов тоже самое
    public abstract class Formula
    {
        public abstract IEnumerable<ObjectVariable> FreeObjectVariables { get; }

        public abstract override string ToString();
    }
}