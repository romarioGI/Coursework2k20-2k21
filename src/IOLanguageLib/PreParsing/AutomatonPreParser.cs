using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public class AutomatonPreParser : PreParser
    {
        protected override IEnumerable<Symbol> Do(PreParsingContext context)
        {
            var automaton = PreParserAutomatonFactory.GetInstance();

            return automaton.Run(context);
        }
    }
}