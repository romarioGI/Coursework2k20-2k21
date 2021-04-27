using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace LogicLanguageLib.Words
{
    public interface IWord
    {
        public  IEnumerable<ObjectVariable> FreeObjectVariables { get; }
    }
}