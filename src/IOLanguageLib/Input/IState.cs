namespace IOLanguageLib.Input
{
    public interface IState<TIn, TOut>
    {
        public (IState<TIn, TOut>, TOut) Next(TIn input);
    }
}