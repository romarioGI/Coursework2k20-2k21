using LogicLanguageLib.Words;

namespace IOLanguageLib.Output
{
    public interface IFormulaPrinter
    {
        public string ToString(Formula formula);
    }
}