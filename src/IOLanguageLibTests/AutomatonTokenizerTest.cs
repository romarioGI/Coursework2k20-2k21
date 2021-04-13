using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Exceptions;
using IOLanguageLib.Input;
using LogicLanguageLib.Alphabet;
using Xunit;

namespace IOLanguageLibTests
{
    public class AutomatonTokenizerTest
    {
        public static IEnumerable<object[]> CorrectTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "abc x y z",
                    new Symbol[]
                    {
                        new Letter('a'), new Letter('b'), new Letter('c'), new Space(), new Letter('x'), new Space(),
                        new Letter('y'), new Space(), new Letter('z')
                    }
                },
                new object[]
                {
                    "123 12 1 x0",
                    new Symbol[]
                    {
                        new Digit('1'), new Digit('2'), new Digit('3'), new Space(), new Digit('1'), new Digit('2'),
                        new Space(), new Digit('1'), new Space(), new Letter('x'), new Digit('0')
                    }
                },
                new object[]
                {
                    "(\\exists x_0)(\\forall x_1)(x_0 * x_1 + a / 1 - 5 > 123 \\land (\\lnot x_0 > x_1 \\lor (x_0 < x_1 \\to x_0 = x_1)))",
                    new Symbol[]
                    {
                        new LeftBracket(), new ExistentialQuantifier(), new Space(), new Letter('x'), new Underlining(),
                        new Digit('0'), new RightBracket(), new LeftBracket(), new UniversalQuantifier(), new Space(),
                        new Letter('x'), new Underlining(), new Digit('1'), new RightBracket(), new LeftBracket(),
                        new Letter('x'), new Underlining(), new Digit('0'), new Space(), new Multiplication(),
                        new Space(), new Letter('x'), new Underlining(), new Digit('1'), new Space(), new Addition(),
                        new Space(), new Letter('a'), new Space(), new Division(), new Space(), new Digit('1'),
                        new Space(), new Minus(), new Space(), new Digit('5'), new Space(), new MorePredicate(),
                        new Space(), new Digit('1'),
                        new Digit('2'), new Digit('3'), new Space(), new Conjunction(), new Space(), new LeftBracket(),
                        new Negation(), new Space(), new Letter('x'), new Underlining(), new Digit('0'), new Space(),
                        new MorePredicate(), new Space(), new Letter('x'), new Underlining(), new Digit('1'),
                        new Space(), new Disjunction(), new Space(), new LeftBracket(), new Letter('x'),
                        new Underlining(), new Digit('0'), new Space(), new LessPredicate(), new Space(),
                        new Letter('x'), new Underlining(), new Digit('1'), new Space(), new Implication(), new Space(),
                        new Letter('x'), new Underlining(), new Digit('0'), new Space(), new EqualityPredicate(),
                        new Space(), new Letter('x'), new Underlining(), new Digit('1'), new RightBracket(),
                        new RightBracket(), new RightBracket()
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
        public void AutomatonTokenizer_CorrectInput(string input, IEnumerable<Symbol> expected)
        {
            var tokenizer = new AutomatonTokenizer();
            var actual = tokenizer.Tokenize(input);

            Assert.Equal(expected.ToArray(), actual.ToArray());
        }

        [Theory]
        [MemberData(nameof(UnexpectedEndOfInputTestData))]
        public void AutomatonTokenizer_UnexpectedEndOfInput(string input)
        {
            var tokenizer = new AutomatonTokenizer();
            var action = new Action(() => tokenizer.Tokenize(input).ToArray());

            Assert.Throws<UnexpectedEndOfInput>(action);
        }

        [Theory]
        [InlineData("%", 0)]
        [InlineData("\\Forall", 1)]
        [InlineData("\\exist x_0", 6)]
        public void AutomatonTokenizer_UnexpectedCharacter(string input, int expectedIndexOfError)
        {
            var tokenizer = new AutomatonTokenizer();
            var action = new Action(() => tokenizer.Tokenize(input).ToArray());

            var exception = Assert.Throws<UnexpectedCharacter>(action);

            Assert.Equal(expectedIndexOfError, exception.IndexOfError);
        }
    }
}