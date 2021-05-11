namespace IOLanguageLib.Alphabet
{
    public interface IOperator
    {
        public byte Arity { get; }

        public Notation Notation { get; }
    }
    
    public enum Notation
    {
        Function,
        Postfix,
        Prefix,
        Infix
    }
}