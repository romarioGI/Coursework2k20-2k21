namespace IOLanguageLib.Alphabet
{
    public sealed class Exponentiation : Function
    {
        public Exponentiation() : base("^")
        {
        }

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Left;

        public override Notation Notation => Notation.Infix;
    }
}