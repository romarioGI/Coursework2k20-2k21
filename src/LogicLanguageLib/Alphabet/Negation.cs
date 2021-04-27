namespace LogicLanguageLib.Alphabet
{
    public sealed class Negation : PropositionalConnective
    {
        protected override string DefaultRepresentation => "¬";

        public override byte Arity => 1;

        public override Associativity Associativity => Associativity.Right;

        public override Notation Notation => Notation.Prefix;
    }
}