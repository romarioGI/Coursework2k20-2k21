using System.Collections.Generic;
using IOLib.Exceptions;
using IOLib.Input.PrefixTree;

namespace IOLib.Input
{
    public class Tokenizer : ITokenizer
    {
        private readonly PrefixTreeNode _prefixTreeRoot = PrefixTreeRootFactory.GetInstance(Lexemes.All);

        public IEnumerable<Token> Tokenize(string input)
        {
            return ToTokens(ToLexemes(input));
        }

        private IEnumerable<Lexeme> ToLexemes(string input)
        {
            var node = _prefixTreeRoot;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
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
                    throw new UnexpectedCharacter(i);
                }
            }

            if (!node.Equals(_prefixTreeRoot))
                throw new UnexpectedEndOfInput();
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
    }
}