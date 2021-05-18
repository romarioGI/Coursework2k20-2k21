using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    public interface IWord
    {
        //TODO проверить, чтобы было Unique везде
        public  IEnumerable<ObjectVariable> FreeObjectVariables { get; }
    }
}