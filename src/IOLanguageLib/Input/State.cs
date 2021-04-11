using System.Collections.Generic;

namespace IOLanguageLib.Input
{
    public class State<TIn, TOut> : IState<TIn, TOut>
    {
        private readonly TOut _errorOutput;
        private readonly ErrorState<TIn, TOut> _errorState;
        private readonly IDictionary<TIn, (IState<TIn, TOut>, TOut)> _next;

        public State(ErrorState<TIn, TOut> errorState, TOut errorOutput)
        {
            _next = new Dictionary<TIn, (IState<TIn, TOut>, TOut)>();
            _errorState = errorState;
            _errorOutput = errorOutput;
        }

        public (IState<TIn, TOut>, TOut) Next(TIn input)
        {
            return _next.ContainsKey(input) ? _next[input] : (_errorState, _errorOutput);
        }

        public void AddNext(TIn input, IState<TIn, TOut> nextState, TOut output)
        {
            _next[input] = (nextState, output);
        }
    }
}