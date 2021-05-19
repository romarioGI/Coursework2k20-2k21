using IOLib.Language;

namespace IOLib.Input
{
    public interface IParser
    {
        public Formula Parse(Word input);
    }
}