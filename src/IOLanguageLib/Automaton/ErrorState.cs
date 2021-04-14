namespace IOLanguageLib.Automaton
{
    public class ErrorState<TIn, TOut> : IState<TIn, TOut>
    {
        private readonly TOut _output;

        public ErrorState(TOut output)
        {
            _output = output;
        }

        public (IState<TIn, TOut>, TOut) Next(TIn input)
        {
            return (this, _output);
        }
    }
}