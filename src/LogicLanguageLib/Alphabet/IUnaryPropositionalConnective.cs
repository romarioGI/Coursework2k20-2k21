namespace LogicLanguageLib.Alphabet
{
    public interface IUnaryPropositionalConnective : IPropositionalConnective
    {
        byte IPropositionalConnective.Arity => 1;
    }
}