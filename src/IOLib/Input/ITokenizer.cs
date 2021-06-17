using System.Collections.Generic;

namespace IOLib.Input
{
    public interface ITokenizer
    {
        public IEnumerable<Token> Tokenize(string input);
    }
}