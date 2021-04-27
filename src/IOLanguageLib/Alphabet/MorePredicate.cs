namespace IOLanguageLib.Alphabet
{
    public sealed class MorePredicate : Predicate
    {
        public MorePredicate() : base(">")
        {
        }

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Left;

        public override Notation Notation => Notation.Infix;
    }
}