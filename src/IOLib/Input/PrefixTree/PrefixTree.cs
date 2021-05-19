using System.Collections.Generic;
using System.Linq;

namespace IOLib.Input.PrefixTree
{
    internal class PrefixTree
    {
        public readonly PrefixTreeNode Root;

        public PrefixTree(IEnumerable<Lexeme> lexemes)
        {
            Root = new PrefixTreeNode();

            foreach (var lexeme in lexemes)
                Add(lexeme);
        }

        private void Add(Lexeme lexeme)
        {
            var node = lexeme.String.Aggregate(Root, (current, c) => current.Add(c));
            node.Lexeme = lexeme;
        }
    }
}