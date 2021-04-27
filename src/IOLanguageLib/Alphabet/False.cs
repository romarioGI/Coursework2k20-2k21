namespace IOLanguageLib.Alphabet
{
    public sealed class False : Predicate
    {
        public False() : base("FALSE")
        {
        }

        public override byte Arity => 0;

        public override Associativity Associativity => Associativity.Left;

        public override Notation Notation => Notation.Function;
    }
}