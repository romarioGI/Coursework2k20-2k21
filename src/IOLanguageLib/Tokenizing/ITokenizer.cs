using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Tokenizing
{
    public interface ITokenizer
    {
        public IEnumerable<Symbol> Tokenize(string input);
    }
}