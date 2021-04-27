using System;
using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.PreParsing
{
    //TODO test, maybe integration
    //TODO определять приоритет операций на уровне алфавита или на уровне парсера?
    public class ReversePolishNotationPreParser : PreParser
    {
        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            var context = new SymbolContext(input);

            return PreParse(context);
        }

        private static IEnumerable<Symbol> PreParse(SymbolContext context)
        {
            throw new NotImplementedException();
        }
    }
}