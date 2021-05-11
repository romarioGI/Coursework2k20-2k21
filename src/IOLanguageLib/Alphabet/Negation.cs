namespace IOLanguageLib.Alphabet
{
    public sealed class Negation : PropositionalConnective
    {
        protected override string DefaultRepresentation => "¬";

        public override byte Arity => 1;

        public override Notation Notation => Notation.Prefix;
    }
}