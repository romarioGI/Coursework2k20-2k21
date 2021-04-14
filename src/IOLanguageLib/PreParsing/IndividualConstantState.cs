using IOLanguageLib.Automaton;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class IndividualConstantState: IState<PreParsingContext, Symbol>
    {
        public (IState<PreParsingContext, Symbol>, Symbol) Next(PreParsingContext input)
        {
            throw new System.NotImplementedException();
        }
    }
}