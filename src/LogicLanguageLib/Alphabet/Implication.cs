namespace LogicLanguageLib.Alphabet
{
    public sealed class Implication : Symbol, IPropositionalConnective
    {
        protected override string DefaultRepresentation => "→";

        public byte Arity => 2;
    }
}