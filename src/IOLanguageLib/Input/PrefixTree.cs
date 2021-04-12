using System;
using System.Collections.Generic;
using System.Linq;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Input
{
    public class PrefixTree
    {
        public readonly PrefixTreeNode Root;

        public PrefixTree(IEnumerable<(string, Symbol)> words)
        {
            Root = new PrefixTreeNode();

            foreach (var (word, symbol) in words)
            {
                if (word is null)
                    throw new ArgumentException("all word must not be null");
                if (symbol is null)
                    throw new ArgumentException("all symbols must not be null");

                AddWord(word, symbol);
            }
        }

        private void AddWord(string word, Symbol symbol)
        {
            var node = word.Aggregate(Root, (current, c) => current.Add(c));
            node.Symbol = symbol;
        }
    }

    public class PrefixTreeNode
    {
        private readonly Dictionary<char, PrefixTreeNode> _children = new();

        public PrefixTreeNode()
        {
            Symbol = null;
        }

        public IReadOnlyDictionary<char, PrefixTreeNode> Children => _children;

        public Symbol Symbol { get; set; }

        public bool IsFinal => !(Symbol is null);

        public PrefixTreeNode Add(char c)
        {
            if (!_children.ContainsKey(c))
                _children[c] = new PrefixTreeNode();

            return _children[c];
        }
    }
}