using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public override string ToString()
        {
            return string.Join("", _tokens.Select(t => t.ToString()));
        }
    }
}