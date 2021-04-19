namespace LogicLanguageLib.Alphabet
{
    public sealed class Negation : Symbol, IPropositionalConnective
    {
        protected override string DefaultRepresentation => "¬";

        public byte Arity => 2;
    }
}