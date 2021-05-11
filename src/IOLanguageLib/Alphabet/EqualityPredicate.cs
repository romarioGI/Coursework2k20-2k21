namespace IOLanguageLib.Alphabet
{
    public sealed class EqualityPredicate : Predicate
    {
        public EqualityPredicate() : base("=")
        {
        }

        public override byte Arity => 2;

        public override Notation Notation => Notation.Infix;
    }
}