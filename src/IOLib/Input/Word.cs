using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IOLib.Input
{
    public class Word : IEnumerable<Token>
    {
        private readonly IReadOnlyList<Token> _tokens;

        public Word(IEnumerable<Token> tokens)
        {
            _tokens = tokens.ToList();
        }

        public Token this[int index] => _tokens[index];

        public int Length => _tokens.Count;

        public Token Last => _tokens[^1];

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