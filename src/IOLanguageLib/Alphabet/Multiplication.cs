namespace IOLanguageLib.Alphabet
{
    public sealed class Multiplication : Function
    {
        public Multiplication() : base("*")
        {
        }

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Left;

        public override Notation Notation => Notation.Infix;
    }
}