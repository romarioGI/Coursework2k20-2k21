namespace IOLanguageLib.Alphabet
{
    public sealed class Implication : PropositionalConnective
    {
        protected override string DefaultRepresentation => "→";

        public override byte Arity => 2;

        public override Notation Notation => Notation.Infix;
    }
}