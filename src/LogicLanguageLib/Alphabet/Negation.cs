namespace LogicLanguageLib.Alphabet
{
    public sealed class Negation : Symbol, IUnaryPropositionalConnective
    {
        protected override string DefaultRepresentation => "¬";
    }
}