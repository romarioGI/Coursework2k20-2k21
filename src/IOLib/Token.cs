namespace IOLib
{
    public class Token
    {
        public readonly int Index;
        public readonly Symbol Symbol;

        public Token(Symbol symbol, int index)
        {
            Symbol = symbol;
            Index = index;
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}