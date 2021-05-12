namespace IOLanguageLib.Alphabet
{
    public sealed class LeftBracket : Symbol
    {
        protected override string DefaultRepresentation => "(";
    }

    public sealed class TermLeftBracket : Symbol
    {
        public TermLeftBracket(LeftBracket leftBracket)
        {
            LeftBracket = leftBracket;
        }

        public LeftBracket LeftBracket { get; }

        protected override string DefaultRepresentation => $"\\t_{LeftBracket}";
    }
}