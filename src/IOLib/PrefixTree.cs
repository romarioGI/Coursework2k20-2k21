using System.Collections.Generic;
using System.Linq;

namespace IOLib
{
    internal class PrefixTree
    {
        public readonly PrefixTreeNode Root;

        public PrefixTree(IEnumerable<Lexeme> lexemes)
        {
            Root = new PrefixTreeNode();

            foreach (var lexeme in lexemes)
                Add(lexeme.String, lexeme.Symbol);
        }

        private void Add(string s, Symbol symbol)
        {
            var node = s.Aggregate(Root, (current, c) => current.Add(c));
            node.Symbol = symbol;
        }
    }
}