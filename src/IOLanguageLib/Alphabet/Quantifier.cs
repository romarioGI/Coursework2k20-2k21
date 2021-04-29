namespace IOLanguageLib.Alphabet
{
    public abstract class Quantifier : Symbol, IOperator
    {
        public abstract byte Arity { get; }

        public Associativity Associativity => Associativity.Right;

        public abstract Notation Notation { get; }
    }
}