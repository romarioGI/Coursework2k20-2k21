using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    public abstract class AbstractPreParser
    {
        public IEnumerable<Symbol> PreParse(IEnumerable<Symbol> input)
        {
            var context = new PreParsingContext(input);

            return PreParse(context);
        }

        protected abstract IEnumerable<Symbol> PreParse(PreParsingContext context);
    }
}