namespace LogicLanguageLib.Alphabet
{
    public sealed class Disjunction : PropositionalConnective
    {
        protected override string DefaultRepresentation => "∨";

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Left;

        public override Notation Notation => Notation.Infix;
    }
}