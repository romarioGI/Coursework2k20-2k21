using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Parsing;
using IOLanguageLib.Parsing.PreParsing;
using IOLanguageLib.Tokenizing;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Translating
{
    public class LazyTranslator : ITranslator
    {
        private readonly PreParser[] _abstractPreParsers;
        private readonly FormulaParser _parser;
        private readonly ITokenizer _tokenizer;

        public LazyTranslator(ITokenizer tokenizer, FormulaParser parser, params PreParser[] abstractPreParsers)
        {
            _tokenizer = tokenizer;
            _parser = parser;
            _abstractPreParsers = abstractPreParsers;
        }

        public Formula Translate(string input)
        {
            var symbols = Tokenize(input);
            var symbolsWithPreParse = PreParse(symbols);
            var formula = Parse(symbolsWithPreParse);

            return formula;
        }

        private IEnumerable<Symbol> Tokenize(string input)
        {
            return _tokenizer.Tokenize(input);
        }

        private IEnumerable<Symbol> PreParse(IEnumerable<Symbol> tokens)
        {
            return _abstractPreParsers
                .Aggregate(tokens, PreParse);
        }

        private static IEnumerable<Symbol> PreParse(IEnumerable<Symbol> tokens, PreParser preParser)
        {
            return preParser
                .Parse(tokens);
        }

        private Formula Parse(IEnumerable<Symbol> tokens)
        {
            return _parser.Parse(tokens);
        }
    }
}