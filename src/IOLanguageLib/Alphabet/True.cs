namespace IOLanguageLib.Alphabet
{
    public sealed class True : Predicate
    {
        public True() : base("TRUE")
        {
        }

        public override byte Arity => 0;

        public override Associativity Associativity => Associativity.Left;
        
        public override Notation Notation => Notation.Prefix;
    }
}