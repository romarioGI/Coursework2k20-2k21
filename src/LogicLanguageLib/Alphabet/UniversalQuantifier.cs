namespace LogicLanguageLib.Alphabet
{
    public sealed class UniversalQuantifier : Quantifier
    {
        protected override string DefaultRepresentation => "∀";

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Right;

        public override Notation Notation => Notation.Prefix;
    }
}