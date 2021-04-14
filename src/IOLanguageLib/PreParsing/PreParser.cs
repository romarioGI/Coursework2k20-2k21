using System.Collections.Generic;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.PreParsing
{
    //TODO tests
    public abstract class PreParser
    {
        public IEnumerable<Symbol> Do(IEnumerable<Symbol> input)
        {
            var context = new PreParsingContext(input);

            return Do(context);
        }

        protected abstract IEnumerable<Symbol> Do(PreParsingContext context);
    }
}