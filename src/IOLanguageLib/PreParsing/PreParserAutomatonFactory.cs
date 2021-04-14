using IOLanguageLib.Alphabet;
using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public static class PreParserAutomatonFactory
    {
        internal static readonly EmptySymbol EmptySymbol = new();
        internal static readonly ErrorSymbol ErrorSymbol = new();
        internal static readonly ErrorState ErrorState = new();
        internal static readonly InitialState InitialState = new();
        internal static readonly IndividualConstantState IndividualConstantState = new();
        internal static readonly ObjectVariableState ObjectVariableState = new();

        public static ContextAutomaton<PreParsingContext, Symbol> GetInstance()
        {
            var finalStates = new[] {InitialState};

            return new ContextAutomaton<PreParsingContext, Symbol>(InitialState, finalStates);
        }
    }
}