namespace IOLanguageLib.Alphabet
{
    public sealed class RightBracket : Symbol
    {
        protected override string DefaultRepresentation => ")";
    }

    public sealed class TermRightBracket : Symbol
    {
        public TermRightBracket(RightBracket rightBracket)
        {
            RightBracket = rightBracket;
        }

        public RightBracket RightBracket { get; }

        protected override string DefaultRepresentation => $"\\t_{RightBracket}";
    }
}