namespace IOLanguageLib.Alphabet
{
    public interface IOperator
    {
        public byte Arity { get; }

        public Associativity Associativity { get; }

        public Notation Notation { get; }
    }

    public enum Associativity
    {
        Left,
        Right
    }
    
    public enum Notation
    {
        Function,
        Postfix,
        Prefix,
        Infix
    }
}