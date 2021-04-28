using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;

namespace IOLanguageLib.Output
{
    //TODO test
    public class FormulaConverterLikeTeX : IConverter<Formula, string>
    {
        private static readonly ConverterToSymbols Converter = new();
        private static readonly SymbolConverterLikeTeX SymbolConverter = new();

        public string Convert(Formula formula)
        {
            var symbols = Converter.Convert(formula);
            var strings = symbols.Select(ToString);
            var result = string.Join("", strings);

            return result;
        }

        private static string ToString(Symbol symbol)
        {
            return SymbolConverter.Convert(symbol);
        }
    }
}