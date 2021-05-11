namespace IOLanguageLib.Alphabet
{
    public abstract class Quantifier : Symbol, IOperator
    {
        public abstract byte Arity { get; }

        public abstract Notation Notation { get; }
    }
}