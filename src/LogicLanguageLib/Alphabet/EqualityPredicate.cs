namespace LogicLanguageLib.Alphabet
{
    public sealed class EqualityPredicate : Predicate
    {
        public EqualityPredicate() : base("=")
        {
        }

        public override byte Arity => 2;

        public override Associativity Associativity => Associativity.Left;
        
        public override Notation Notation => Notation.Infix;
    }
}