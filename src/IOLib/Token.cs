namespace IOLib
{
    public class Token
    {
        private readonly int _index;
        private readonly Symbol _symbol;

        public Token(Symbol symbol, int index)
        {
            _symbol = symbol;
            _index = index;
        }
    }
}