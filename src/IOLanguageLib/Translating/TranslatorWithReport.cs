using System;
using System.Linq;
using System.Transactions;
using IOLanguageLib.Exceptions;
using IOLanguageLib.Parsing;
using IOLanguageLib.PreParsing;
using IOLanguageLib.Tokenizing;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Translating
{
    //TODO integration tests
    public class TranslatorWithReport : ITranslator
    {
        private readonly AbstractPreParser[] _abstractPreParsers;
        private readonly IParser _parser;
        private readonly IReportWriter _reportWriter;
        private readonly ITokenizer _tokenizer;

        public TranslatorWithReport(IReportWriter reportWriter, ITokenizer tokenizer, IParser parser,
            params AbstractPreParser[] abstractPreParsers)
        {
            _reportWriter = reportWriter;
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

        private Symbol[] Tokenize(string input)
        {
            _reportWriter.WriteLine("Tokenizing start.");
            try
            {
                return _tokenizer.Tokenize(input).ToArray();
            }
            catch (Exception e)
            {
                _reportWriter.WriteError("Tokenize error.", input, e);
                throw new TransactionException();
            }
            finally
            {
                _reportWriter.WriteLine("Tokenizing completed.");
            }
        }

        private Symbol[] PreParse(Symbol[] tokens)
        {
            return _abstractPreParsers
                .Aggregate(tokens, PreParse);
        }

        private Symbol[] PreParse(Symbol[] tokens, AbstractPreParser preParser)
        {
            var preParserName = preParser.GetType();
            _reportWriter.WriteLine($"Pre-parse start({preParserName}).");
            try
            {
                return preParser
                    .PreParse(tokens)
                    .ToArray();
            }
            catch (Exception e)
            {
                _reportWriter.WriteError($"Pre-parse error({preParserName}).", tokens, e);
                throw new TransactionException();
            }
            finally
            {
                _reportWriter.WriteLine($"Pre-parsing completed({preParserName}).");
            }
        }

        private Formula Parse(Symbol[] tokens)
        {
            _reportWriter.WriteLine("Parsing start.");
            try
            {
                return _parser.Parse(tokens);
            }
            catch (InputException e)
            {
                _reportWriter.WriteError("Parse error.", tokens, e);
                throw new TransactionException();
            }
            finally
            {
                _reportWriter.WriteLine("Parsing completed.");
            }
        }
    }
}