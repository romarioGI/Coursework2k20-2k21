using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Input
{
    public static class TokensAutomatonFactory
    {
        private static readonly EmptySymbol EmptySymbol = new();
        private static readonly ErrorSymbol ErrorSymbol = new();
        private static readonly ErrorState<char, Symbol> ErrorState = new(EmptySymbol);

        private static readonly State<char, Symbol> InitialState;

        /// No word should be the beginning (prefix) of another.
        static TokensAutomatonFactory()
        {
            var tokens = new (string, Symbol)[]
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
            var prefixTree = new PrefixTree(tokens);
            InitialState = new State<char, Symbol>(ErrorState, ErrorSymbol);
            BuildState(InitialState, prefixTree.Root);
            AddLettersAndDigits();
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
                    BuildState(nextState, child);
                    output = EmptySymbol;
                }

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