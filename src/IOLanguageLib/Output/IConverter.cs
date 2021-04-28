namespace IOLanguageLib.Output
{
    public interface IConverter<in TIn, out TOut>
    {
        public TOut Convert(TIn input);
    }
}