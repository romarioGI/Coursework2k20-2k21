using System.Collections.Generic;

namespace IOLib
{
    internal static class PrefixTreeRootFactory
    {
        public static PrefixTreeNode GetInstance(IEnumerable<Lexeme> lexemes)
        {
            return new PrefixTree(lexemes).Root;
        }
    }
}