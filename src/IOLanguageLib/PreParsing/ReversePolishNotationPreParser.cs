using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    //TODO test, maybe integration
    //TODO перанды должны тоже помещаться в стек, во-первых, чтобы в опз они были в обратном порядке, во-вторых, чтобы решаить задачу с кванторами
    public class ReversePolishNotationPreParser: AbstractPreParser
    {
        protected override IEnumerable<Symbol> PreParse(PreParsingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}