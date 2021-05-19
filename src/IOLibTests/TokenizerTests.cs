using System;
using System.Collections.Generic;
using System.Linq;
using IOLib;
using IOLib.Exceptions;
using Xunit;

namespace IOLibTests
{
    public class TokenizerTests
    {
        public static IEnumerable<object[]> CorrectTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "abc x y z",
                    new[]
                    {
                        Alphabet.Letter('a'), Alphabet.Letter('b'), Alphabet.Letter('c'), Alphabet.Space,
                        Alphabet.Letter('x'), Alphabet.Space,
                        Alphabet.Letter('y'), Alphabet.Space, Alphabet.Letter('z')
                    }
                },
                new object[]
                {
                    "123 12 1 x0",
                    new[]
                    {
                        Alphabet.Digit('1'), Alphabet.Digit('2'), Alphabet.Digit('3'), Alphabet.Space,
                        Alphabet.Digit('1'), Alphabet.Digit('2'),
                        Alphabet.Space, Alphabet.Digit('1'), Alphabet.Space, Alphabet.Letter('x'), Alphabet.Digit('0')
                    }
                },
                new object[]
                {
                    "(\\exists x_0)(\\forall x_1)(x_0 * x_1 + a / 1 - 5 > 123 \\land (\\lnot x_0 > x_1 \\lor (x_0 < x_1 \\to x_0 = x_1)))",
                    new[]
                    {
                        Alphabet.LeftBracket, Alphabet.ExistentialQuantifier, Alphabet.Space, Alphabet.Letter('x'),
                        Alphabet.Underlining,
                        Alphabet.Digit('0'), Alphabet.RightBracket, Alphabet.LeftBracket, Alphabet.UniversalQuantifier,
                        Alphabet.Space,
                        Alphabet.Letter('x'), Alphabet.Underlining, Alphabet.Digit('1'), Alphabet.RightBracket,
                        Alphabet.LeftBracket,
                        Alphabet.Letter('x'), Alphabet.Underlining, Alphabet.Digit('0'), Alphabet.Space,
                        Alphabet.Multiplication,
                        Alphabet.Space, Alphabet.Letter('x'), Alphabet.Underlining, Alphabet.Digit('1'), Alphabet.Space,
                        Alphabet.Plus,
                        Alphabet.Space, Alphabet.Letter('a'), Alphabet.Space, Alphabet.Division, Alphabet.Space,
                        Alphabet.Digit('1'),
                        Alphabet.Space, Alphabet.Minus, Alphabet.Space, Alphabet.Digit('5'), Alphabet.Space,
                        Alphabet.More,
                        Alphabet.Space, Alphabet.Digit('1'),
                        Alphabet.Digit('2'), Alphabet.Digit('3'), Alphabet.Space, Alphabet.Conjunction, Alphabet.Space,
                        Alphabet.LeftBracket,
                        Alphabet.Negation, Alphabet.Space, Alphabet.Letter('x'), Alphabet.Underlining,
                        Alphabet.Digit('0'), Alphabet.Space,
                        Alphabet.More, Alphabet.Space, Alphabet.Letter('x'), Alphabet.Underlining, Alphabet.Digit('1'),
                        Alphabet.Space, Alphabet.Disjunction, Alphabet.Space, Alphabet.LeftBracket,
                        Alphabet.Letter('x'),
                        Alphabet.Underlining, Alphabet.Digit('0'), Alphabet.Space, Alphabet.Less, Alphabet.Space,
                        Alphabet.Letter('x'), Alphabet.Underlining, Alphabet.Digit('1'), Alphabet.Space,
                        Alphabet.Implication, Alphabet.Space,
                        Alphabet.Letter('x'), Alphabet.Underlining, Alphabet.Digit('0'), Alphabet.Space, Alphabet.Equal,
                        Alphabet.Space, Alphabet.Letter('x'), Alphabet.Underlining, Alphabet.Digit('1'),
                        Alphabet.RightBracket,
                        Alphabet.RightBracket, Alphabet.RightBracket
                    }
                }
            };

        public static IEnumerable<object[]> UnexpectedEndOfInputTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "\\"
                },
                new object[]
                {
                    "\\foral"
                },
                new object[]
                {
                    "\\l"
                }
            };

        [Theory]
        [MemberData(nameof(CorrectTestData))]
        public void AutomatonTokenizer_CorrectInput(string input, IEnumerable<Symbol> expectedSymbols)
        {
            var tokenizer = new Tokenizer();
            var actual = tokenizer.Tokenize(input);

            var actualSymbols = actual.Select(t => t.Symbol);

            Assert.Equal(expectedSymbols.ToArray(), actualSymbols.ToArray());
        }

        [Theory]
        [MemberData(nameof(UnexpectedEndOfInputTestData))]
        public void AutomatonTokenizer_UnexpectedEndOfInput(string input)
        {
            var tokenizer = new Tokenizer();
            var func = new Func<object>(() => tokenizer.Tokenize(input).ToArray());

            Assert.Throws<UnexpectedEndOfInput>(func);
        }

        [Theory]
        [InlineData("%", 0)]
        [InlineData("\\Forall", 1)]
        [InlineData("\\exist x_0", 6)]
        public void AutomatonTokenizer_UnexpectedCharacter(string input, int expectedIndexOfError)
        {
            var tokenizer = new Tokenizer();
            var func = new Func<object>(() => tokenizer.Tokenize(input).ToArray());

            var exception = Assert.Throws<UnexpectedCharacter>(func);

            Assert.Equal(expectedIndexOfError, exception.IndexOfError);
        }
    }
}