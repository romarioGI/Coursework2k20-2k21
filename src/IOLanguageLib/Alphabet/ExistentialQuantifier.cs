namespace IOLanguageLib.Alphabet
{
    public sealed class ExistentialQuantifier : Quantifier
    {
        protected override string DefaultRepresentation => "∃";
        
        public override byte Arity => 2;

        public override Notation Notation => Notation.Prefix;
    }
}