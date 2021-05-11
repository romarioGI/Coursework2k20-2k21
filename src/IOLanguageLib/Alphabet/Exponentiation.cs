namespace IOLanguageLib.Alphabet
{
    public sealed class Exponentiation : Function
    {
        public Exponentiation() : base("^")
        {
        }

        public override byte Arity => 2;

        public override Notation Notation => Notation.Infix;
    }
}