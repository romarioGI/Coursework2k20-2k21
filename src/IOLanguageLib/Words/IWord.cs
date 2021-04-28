using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public interface IWord
    {
        public  IEnumerable<ObjectVariable> FreeObjectVariables { get; }
    }
}