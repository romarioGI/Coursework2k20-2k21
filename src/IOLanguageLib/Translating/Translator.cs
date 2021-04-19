using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Parsing;
using IOLanguageLib.PreParsing;
using IOLanguageLib.Tokenizing;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Translating
{
    //TODO integration tests
    public class Translator : ITranslator
    {
        private readonly AbstractPreParser[] _abstractPreParsers;
        private readonly IParser _parser;
        private readonly ITokenizer _tokenizer;

        public Translator(ITokenizer tokenizer, IParser parser, params AbstractPreParser[] abstractPreParsers)
        {
            _tokenizer = tokenizer;
            _parser = parser;
            _abstractPreParsers = abstractPreParsers;
        }

        /*TODO добавить обработку ошибок. У многих ошибок указан индекс "неправильного" символа, для этого нужно
         * распчетать "текущие входные данные". Это можно сделать создавая новую ошибку, в которую передавать
         * возникшую ошибку и текущий вход. Этой ошибкой заменить все остальные ошибки.
        */
        public Formula Translate(string input)
        {
            var symbols = _tokenizer.Tokenize(input);
            var symbolsWithPreParse = PreParse(symbols);
            var formula = _parser.Parse(symbolsWithPreParse);

            return formula;
        }

        private IEnumerable<Symbol> PreParse(IEnumerable<Symbol> tokens)
        {
            return _abstractPreParsers
                .Aggregate(tokens, (current, preParser) => preParser.PreParse(current));
        }
    }
}