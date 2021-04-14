using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Tokenizing
{
    public interface ITokenizer
    {
        public IEnumerable<Symbol> Tokenize(string input);
    }
}