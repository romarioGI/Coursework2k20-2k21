using System.Collections.Generic;

namespace IOLib
{
    public class Tokenizer : ITokenizer
    {
        public Word Tokenize(string input)
        {
            return ToWord(ToTokens(ToLexemes(input)));
        }

        private static IEnumerable<Lexeme> ToLexemes(string input)
        {
            var tree = new PrefixTree(Lexemes.All);
            var node = tree.Root;
            foreach (var c in input)
            {
                var nextNode = node.Next(c);
                if (nextNode is not null)
                    continue;
                if(node.IsFinal)
            }
        }

        private static IEnumerable<Token> ToTokens(IEnumerable<Lexeme> lexemes)
        {
            var index = 0;
            foreach (var lexeme in lexemes)
            {
                yield return new Token(lexeme.Symbol, index);
                index += lexeme.Length;
            }
        }

        private static Word ToWord(IEnumerable<Token> tokens)
        {
            return new(tokens);
        }
    }
}