namespace LogicLanguageLib.Alphabet
{
    public sealed class Exponentiation : Function
    {
        public Exponentiation() : base("^")
        {
        }

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Right;

        public override Notation Notation => Notation.Infix;
    }
}