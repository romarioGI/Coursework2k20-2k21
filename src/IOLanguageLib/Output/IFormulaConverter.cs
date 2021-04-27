using IOLanguageLib.Words;

namespace IOLanguageLib.Output
{
    public interface IFormulaConverter<out T>
    {
        public T Convert(Formula formula);
    }
}