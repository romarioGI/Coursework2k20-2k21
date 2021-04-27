using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public abstract class Formula : IWord
    {
        public bool IsSentence => !FreeObjectVariables.Any();

        public abstract IEnumerable<Formula> SubFormulas { get; }

        public abstract IEnumerable<ObjectVariable> FreeObjectVariables { get; }

        public abstract IEnumerator<Symbol> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract override string ToString();
    }
}