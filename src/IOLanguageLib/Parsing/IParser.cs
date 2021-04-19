using System.Collections.Generic;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Parsing
{
    public interface IParser
    {
        public Formula Parse(IEnumerable<Symbol> symbols);
    }
}