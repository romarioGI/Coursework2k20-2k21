using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public abstract class Formula : IWord
    {
        public abstract IEnumerable<ObjectVariable> FreeObjectVariables { get; }

        public abstract override string ToString();
    }
}