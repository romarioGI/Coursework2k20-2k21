using System.Collections.Generic;

namespace IOLib
{
    internal class PrefixTreeNode
    {
        private readonly Dictionary<char, PrefixTreeNode> _children = new();

        public PrefixTreeNode()
        {
            Symbol = null;
        }

        public Symbol Symbol { get; set; }

        public bool IsFinal => Symbol is not null;

        public PrefixTreeNode Add(char c)
        {
            if (!_children.ContainsKey(c))
                _children[c] = new PrefixTreeNode();

            return _children[c];
        }

        public PrefixTreeNode Next(char c)
        {
            return _children.ContainsKey(c) ? _children[c] : null;
        }
    }
}