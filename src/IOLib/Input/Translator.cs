using IOLib.Language;

namespace IOLib.Input
{
    public class Translator : ITranslator
    {
        private readonly IParser _parser;
        private readonly ITokenizer _tokenizer;

        public Translator(ITokenizer tokenizer, IParser parser)
        {
            _tokenizer = tokenizer;
            _parser = parser;
        }

        public Formula Translate(string input)
        {
            var word = _tokenizer.Tokenize(input);
            var formula = _parser.Parse(word);

            return formula;
        }
    }
}