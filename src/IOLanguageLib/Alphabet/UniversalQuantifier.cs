namespace IOLanguageLib.Alphabet
{
    public sealed class UniversalQuantifier : Quantifier
    {
        protected override string DefaultRepresentation => "∀";

        public override byte Arity => 2;

        public override Notation Notation => Notation.Prefix;
    }
}