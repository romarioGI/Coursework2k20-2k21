using System.Collections.Generic;
using System.Linq;
using LogicLanguageLib.Alphabet;

namespace LogicLanguageLib.Words
{
    public abstract class Formula : IObjectVariablesCollection
    {
        public bool IsSentence => !FreeObjectVariables.Any();

        public abstract IEnumerable<Formula> SubFormulas { get; }
        public abstract IEnumerable<ObjectVariable> FreeObjectVariables { get; }

        public abstract override string ToString();
    }
}