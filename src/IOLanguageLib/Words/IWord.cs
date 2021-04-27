using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public interface IWord: IEnumerable<Symbol>
    {
        public  IEnumerable<ObjectVariable> FreeObjectVariables { get; }
    }
}