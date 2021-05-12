using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.PreParsing
{
    public class BracketPreParser : PreParser
    {
        //TODO
        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            return input;
        }
    }
}