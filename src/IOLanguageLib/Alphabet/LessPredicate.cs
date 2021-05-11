namespace IOLanguageLib.Alphabet
{
    public sealed class LessPredicate : Predicate
    {
        public LessPredicate() : base("<")
        {
        }

        public override byte Arity => 2;

        public override Notation Notation => Notation.Infix;
    }
}