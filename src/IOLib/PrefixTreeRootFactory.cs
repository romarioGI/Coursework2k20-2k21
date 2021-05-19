namespace IOLib
{
    //TODO сериализация
    internal static class PrefixTreeRootFactory
    {
        public static PrefixTreeNode GetInstance()
        {
            return new PrefixTree(Lexemes.All).Root;
        }
    }
}