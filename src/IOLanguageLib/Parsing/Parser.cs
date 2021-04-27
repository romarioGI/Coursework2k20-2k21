using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing
{
    public abstract class Parser<TOut>
    {
        public abstract TOut Parse(IEnumerable<Symbol> input);
    }
}