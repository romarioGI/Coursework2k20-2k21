using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Parsing;
using IOLanguageLib.Parsing.PreParsing;
using Xunit;

namespace IOLanguageLibTests
{
    public class AdditionalSymbolsPreParserTests
    {
        public static IEnumerable<object[]> CorrectTestData =>
            new List<object[]>
            {
                new object[]
                {
                    new Symbol[]
                    {
                        new Letter('a'), new Letter('b'), new Letter('c'), new Space(), new Letter('x'), new Space(),
                        new Letter('y'), new Space(), new Letter('z')
                    },
                    new Symbol[]
                    {
                        new ObjectVariable('a'), new ObjectVariable('b'), new ObjectVariable('c'),
                        new ObjectVariable('x'),
                        new ObjectVariable('y'), new ObjectVariable('z')
                    }
                },
                new object[]
                {
                    new Symbol[]
                    {
                        new Digit('1'), new Digit('2'), new Digit('3'), new Space(), new Digit('1'), new Digit('2'),
                        new Space(), new Digit('1'), new Space(), new Letter('x'), new Digit('0')
                    },
                    new Symbol[]
                    {
                        new IndividualConstant(123), new IndividualConstant(12), new IndividualConstant(1),
                        new ObjectVariable('x'), new IndividualConstant(0)
                    }
                },
                new object[]
                {
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
                    },
                    new Symbol[]
                    {
                        new LeftBracket(), new ExistentialQuantifier(), new ObjectVariable('x', 0), new RightBracket(),
                        new LeftBracket(), new UniversalQuantifier(), new ObjectVariable('x', 1), new RightBracket(),
                        new LeftBracket(), new ObjectVariable('x', 0), new Multiplication(), new ObjectVariable('x', 1),
                        new Addition(), new ObjectVariable('a'), new Division(), new IndividualConstant(1), new Minus(),
                        new IndividualConstant(5), new MorePredicate(), new IndividualConstant(123), new Conjunction(),
                        new LeftBracket(), new Negation(), new ObjectVariable('x', 0), new MorePredicate(),
                        new ObjectVariable('x', 1), new Disjunction(), new LeftBracket(), new ObjectVariable('x', 0),
                        new LessPredicate(), new ObjectVariable('x', 1), new Implication(), new ObjectVariable('x', 0),
                        new EqualityPredicate(), new ObjectVariable('x', 1), new RightBracket(), new RightBracket(),
                        new RightBracket()
                    }
                },
                new object[]
                {
                    new Symbol[]
                    {
                        new Letter('a')
                    },
                    new Symbol[]
                    {
                        new ObjectVariable('a')
                    }
                },
                new object[]
                {
                    new Symbol[]
                    {
                        new Letter('a'), new Underlining(), new Digit('4'), new Digit('2')
                    },
                    new Symbol[]
                    {
                        new ObjectVariable('a', 42)
                    }
                }
            };

        [Theory]
        [MemberData(nameof(CorrectTestData))]
        public void PreParse_CorrectInput(IEnumerable<Symbol> input, IEnumerable<Symbol> expected)
        {
            var preParser = new AdditionalSymbolsPreParser();
            var actual = preParser.Parse(input);

            Assert.Equal(expected.ToArray(), actual.ToArray());
        }
    }
}