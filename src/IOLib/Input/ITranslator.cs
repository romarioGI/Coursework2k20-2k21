using IOLib.Language;

namespace IOLib.Input
{
    public interface ITranslator
    {
        public Formula Translate(string input);
    }
}