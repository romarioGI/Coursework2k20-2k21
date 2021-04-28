using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Words
{
    //TODO логику перевода слова в список символов извлечь в конверторы.
    // во-первых, это избавит код от расширяющего метода на операторы
    // во-вторых, эти правила слишком специфичны для класса, ибо нужно расставлять скобки, пробелы, возожно, оптимизировать выражение, не расставляя какие-то скобки
    public interface IWord
    {
        public  IEnumerable<ObjectVariable> FreeObjectVariables { get; }
    }
}