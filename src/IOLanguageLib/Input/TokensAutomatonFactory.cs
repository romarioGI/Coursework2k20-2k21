using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Input
{
    //TODO test
    public static class TokensAutomatonFactory
    {
        private static readonly EmptySymbol EmptySymbol = new();
        private static readonly ErrorState<char, Symbol> ErrorState = new(EmptySymbol);

        private static readonly State<char, Symbol> InitialState;

        /// No word should be the beginning (prefix) of another.
        static TokensAutomatonFactory()
        {
            var tokens = new (string, Symbol)[]
            {
                ("\\lnot", new Negation()),
                ("\\land", new Conjunction()),
                ("\\lor", new Disjunction()),
                ("\\to", new Implication()),
                ("\\exists", new ExistentialQuantifier()),
                ("\\forall", new UniversalQuantifier()),
                ("\\over", new Division()),
                ("\\cdot", new Multiplication()),
                ("<", new LessPredicate()),
                (">", new MorePredicate()),
                ("=", new EqualityPredicate()),
                ("+", new Addition()),
                ("-", new Minus()),
                ("*", new Multiplication()),
                ("/", new Division()),
                ("^", new Exponentiation()),
                (" ", EmptySymbol)
            };
            var prefixTree = new PrefixTree(tokens);
            InitialState = new State<char, Symbol>(ErrorState, EmptySymbol);
            BuildState(InitialState, prefixTree.Root);
            AddLettersAndDigits();
        }

        private static void BuildState(State<char, Symbol> state, PrefixTreeNode node)
        {
            foreach (var (input, child) in node.Children)
            {
                State<char, Symbol> nextState;

                if (node.IsFinal)
                {
                    nextState = InitialState;
                }
                else
                {
                    nextState = new State<char, Symbol>(ErrorState, EmptySymbol);
                    BuildState(nextState, child);
                }

                var output = node.Symbol;
                state.AddNext(input, nextState, output);
            }
        }

        private static void AddLettersAndDigits()
        {
            for (var c = 'a'; c <= 'z'; c++)
                InitialState.AddNext(c, InitialState, new Letter(c));
            for (var c = '0'; c <= '9'; c++)
                InitialState.AddNext(c, InitialState, new Digit(c));
        }

        public static Automaton<char, Symbol> GetInstance()
        {
            var finalStates = new[] {InitialState};

            return new Automaton<char, Symbol>(InitialState, finalStates);
        }
    }
}