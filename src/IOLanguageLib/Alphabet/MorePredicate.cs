namespace IOLanguageLib.Alphabet
{
    public sealed class MorePredicate : Predicate
    {
        public MorePredicate() : base(">")
        {
        }

        public override byte Arity => 2;

        public override Notation Notation => Notation.Infix;
    }
}