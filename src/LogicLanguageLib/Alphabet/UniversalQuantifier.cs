namespace LogicLanguageLib.Alphabet
{
    public sealed class UniversalQuantifier : Symbol, IQuantifier
    {
        protected override string DefaultRepresentation => "∀";

        public byte Arity => 2;
    }
}