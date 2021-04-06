using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace LogicLanguageLib.Words
{
    public interface IObjectVariablesCollection
    {
        public  IEnumerable<ObjectVariable> FreeObjectVariables { get; }
    }
}