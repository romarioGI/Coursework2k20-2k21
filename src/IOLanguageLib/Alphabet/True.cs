namespace IOLanguageLib.Alphabet
{
    public sealed class True : Predicate
    {
        public True() : base("TRUE")
        {
        }

        public override byte Arity => 0;

        public override Notation Notation => Notation.Function;
    }
}