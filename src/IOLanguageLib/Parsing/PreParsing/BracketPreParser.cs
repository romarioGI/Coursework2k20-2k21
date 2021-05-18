using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.PreParsing
{
    public class BracketPreParser : PreParser
    {
        //TODO tests
        //TODO implementation
        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            return input;
        }
    }
}