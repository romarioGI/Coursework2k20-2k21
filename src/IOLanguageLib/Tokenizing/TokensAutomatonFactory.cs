using System.Collections.Generic;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Tokenizing
{
    internal static class TokensAutomatonFactory
    {
        private static readonly EmptySymbol EmptySymbol = new();
        private static readonly ErrorSymbol ErrorSymbol = new();
        private static readonly ErrorState ErrorState = new();

        private static readonly State<char, Symbol> InitialState;
        
        static TokensAutomatonFactory()
        {
            var tokens = GetTokens();
            var prefixTree = new PrefixTree(tokens);
            InitialState = BuildInitialState(prefixTree);
        }

        /// No word should be the beginning (prefix) of another.
        private static IEnumerable<(string, Symbol)> GetTokens()
        {
            var tokens = new List<(string, Symbol)>
            {
                ("\\exists", new ExistentialQuantifier()),
                ("\\forall", new UniversalQuantifier()),
                ("\\land", new Conjunction()),
                ("\\lnot", new Negation()),
                ("\\lor", new Disjunction()),
                ("\\to", new Implication()),
                ("<", new LessPredicate()),
                (">", new MorePredicate()),
                ("=", new EqualityPredicate()),
                ("+", new Addition()),
                ("-", new Minus()),
                ("*", new Multiplication()),
                ("\\cdot", new Multiplication()),
                ("/", new Division()),
                ("\\over", new Division()),
                ("^", new Exponentiation()),
                (" ", new Space()),
                ("_", new Underlining()),
                ("(", new LeftBracket()),
                (")", new RightBracket())
            };

            for (var c = 'a'; c <= 'z'; c++)
                tokens.Add((c.ToString(), new Letter(c)));
            for (var c = '0'; c <= '9'; c++)
                tokens.Add((c.ToString(), new Digit(c)));

            return tokens;
        }

        private static State<char, Symbol> BuildInitialState(PrefixTree prefixTree)
        {
            var initialState = new State<char, Symbol>(ErrorState, ErrorSymbol);
            BuildState(initialState, prefixTree.Root);

            return initialState;
        }

        private static void BuildState(State<char, Symbol> state, PrefixTreeNode node)
        {
            foreach (var (input, child) in node.Children)
            {
                State<char, Symbol> nextState;
                Symbol output;

                if (child.IsFinal)
                {
                    nextState = InitialState;
                    output = child.Symbol;
                }
                else
                {
                    nextState = new State<char, Symbol>(ErrorState, ErrorSymbol);
                    output = EmptySymbol;

                    BuildState(nextState, child);
                }

                state.AddNext(input, nextState, output);
            }
        }

        public static Automaton<char, Symbol> GetInstance()
        {
            var finalStates = new[] {InitialState};

            return new Automaton<char, Symbol>(InitialState, finalStates);
        }
    }
}