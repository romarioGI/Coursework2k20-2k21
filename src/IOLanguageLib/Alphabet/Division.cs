namespace IOLanguageLib.Alphabet
{
    public sealed class Division : Function
    {
        public Division() : base("/")
        {
        }

        public override byte Arity => 2;

        public override Notation Notation => Notation.Infix;
    }
}