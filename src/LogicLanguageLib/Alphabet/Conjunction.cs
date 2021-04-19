namespace LogicLanguageLib.Alphabet
{
    public sealed class Conjunction : Symbol, IPropositionalConnective
    {
        protected override string DefaultRepresentation => "&";

        public byte Arity => 2;
    }
}