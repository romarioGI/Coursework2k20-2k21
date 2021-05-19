using System.Collections.Generic;
using IOLib.Input;

namespace IOLib.PrefixTree
{
    internal static class PrefixTreeRootFactory
    {
        public static PrefixTreeNode GetInstance(IEnumerable<Lexeme> lexemes)
        {
            return new PrefixTree(lexemes).Root;
        }
    }
}