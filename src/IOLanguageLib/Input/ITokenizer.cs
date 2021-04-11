using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Input
{
    public interface ITokenizer
    {
        public IEnumerable<Symbol> Tokenize(string input);
    }
}