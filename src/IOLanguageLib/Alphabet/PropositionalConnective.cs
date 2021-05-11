namespace IOLanguageLib.Alphabet
{
    public abstract class PropositionalConnective : Symbol, IOperator
    {
        public abstract byte Arity { get; }

        public abstract Notation Notation { get; }
    }
}