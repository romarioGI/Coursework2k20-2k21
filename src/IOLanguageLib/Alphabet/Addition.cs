namespace IOLanguageLib.Alphabet
{
    public sealed class Addition : Function
    {
        public Addition() : base("+")
        {
        }

        public override byte Arity => 2;

        public override Notation Notation => Notation.Infix;
    }
}