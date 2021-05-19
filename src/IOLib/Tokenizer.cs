using System;
using System.Collections.Generic;

namespace IOLib
{
    //TODO tests
    public class Tokenizer : ITokenizer
    {
        private readonly PrefixTreeNode _prefixTreeRoot = PrefixTreeRootFactory.GetInstance();

        public Word Tokenize(string input)
        {
            return ToWord(ToTokens(ToLexemes(input)));
        }

        private IEnumerable<Lexeme> ToLexemes(string input)
        {
            var node = _prefixTreeRoot;
            foreach (var c in input)
                if (node.Children.ContainsKey(c))
                {
                    node = node.Children[c];
                    if (!node.IsFinal)
                        continue;
                    yield return node.Lexeme;
                    node = _prefixTreeRoot;
                }
                else
                {
                    throw new NotImplementedException("UnexpectedCharException");
                }

            if (!node.Equals(_prefixTreeRoot))
                throw new NotImplementedException("UnexpectedEndOfInput");
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