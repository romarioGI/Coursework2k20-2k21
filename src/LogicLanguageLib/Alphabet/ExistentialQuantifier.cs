namespace LogicLanguageLib.Alphabet
{
    public sealed class ExistentialQuantifier : Symbol, IQuantifier
    {
        protected override string DefaultRepresentation => "∃";
    }
}