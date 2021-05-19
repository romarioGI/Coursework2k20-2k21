using System.Collections.Generic;
using IOLib.Input;

namespace IOLib.PrefixTree
{
    internal class PrefixTreeNode
    {
        private readonly Dictionary<char, PrefixTreeNode> _children = new();

        public PrefixTreeNode()
        {
            Lexeme = null;
        }

        public IReadOnlyDictionary<char, PrefixTreeNode> Children => _children;

        public Lexeme Lexeme { get; set; }

        public bool IsFinal => Lexeme is not null;

        public PrefixTreeNode Add(char c)
        {
            if (!_children.ContainsKey(c))
                _children[c] = new PrefixTreeNode();

            return _children[c];
        }
    }
}