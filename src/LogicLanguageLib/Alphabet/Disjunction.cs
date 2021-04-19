namespace LogicLanguageLib.Alphabet
{
    public sealed class Disjunction : Symbol, IPropositionalConnective
    {
        protected override string DefaultRepresentation => "∨";

        public byte Arity => 2;
    }
}