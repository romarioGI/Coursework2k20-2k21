namespace LogicLanguageLib.Alphabet
{
    public sealed class Implication : Symbol, IBinaryPropositionalConnective
    {
        protected override string DefaultRepresentation => "→";
    }
}