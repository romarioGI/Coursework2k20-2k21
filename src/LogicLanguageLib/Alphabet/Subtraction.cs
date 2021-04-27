namespace LogicLanguageLib.Alphabet
{
    public sealed class Subtraction : Function
    {
        public Subtraction() : base("-")
        {
        }

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Left;

        public override Notation Notation => Notation.Infix;
    }
}