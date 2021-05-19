using System.Collections.Generic;
using IOLib.Language;

namespace IOLib.Input
{
    public interface IParser
    {
        public Formula Parse(IEnumerable<Token> input);
    }
}