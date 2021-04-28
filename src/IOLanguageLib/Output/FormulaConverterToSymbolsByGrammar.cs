using System.Collections.Generic;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;

namespace IOLanguageLib.Output
{
    public class FormulaConverterToSymbolsByGrammar: IConverter<Formula, IEnumerable<Symbol>>
    {
        public IEnumerable<Symbol> Convert(Formula formula)
        {
            return formula;
        }
    }
}