using System.Collections;
using System.Collections.Generic;

namespace IOLib
{
    public class Word : IEnumerable<Token>
    {
        private readonly IEnumerable<Token> _tokens;

        public Word(IEnumerable<Token> tokens)
        {
            _tokens = tokens;
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}