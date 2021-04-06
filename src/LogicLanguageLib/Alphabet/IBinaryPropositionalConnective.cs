namespace LogicLanguageLib.Alphabet
{
    public interface IBinaryPropositionalConnective : IPropositionalConnective
    {
        byte IPropositionalConnective.Arity => 2;
    }
}