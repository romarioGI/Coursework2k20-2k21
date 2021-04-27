namespace LogicLanguageLib.Alphabet
{
    public sealed class UnaryMinus : Function
    {
        public UnaryMinus() : base("-")
        {
        }

        public override byte Arity => 1;

        public override Associativity Associativity => Associativity.Right;

        public override Notation Notation => Notation.Prefix;
    }
}