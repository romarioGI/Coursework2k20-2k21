using System.Collections.Generic;

namespace IOLib.Input.PrefixTree
{
    internal static class PrefixTreeRootFactory
    {
        public static PrefixTreeNode GetInstance(IEnumerable<Lexeme> lexemes)
        {
            return new Input.PrefixTree.PrefixTree(lexemes).Root;
        }
    }
}