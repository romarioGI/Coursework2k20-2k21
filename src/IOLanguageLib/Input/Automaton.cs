using System.Collections.Generic;
using System.Linq;

namespace IOLanguageLib.Input
{
    public class Automaton<TIn, TOut>
    {
        public Automaton(IState<TIn, TOut> initialState, IEnumerable<IState<TIn, TOut>> finalStates)
        {
            InitialState = initialState;
            CurrentState = InitialState;
            FinalStates = finalStates.ToHashSet();
        }

        public IState<TIn, TOut> InitialState { get; }

        public IState<TIn, TOut> CurrentState { get; private set; }

        public IEnumerable<IState<TIn, TOut>> FinalStates { get; }

        public bool InFinalState => FinalStates.Contains(CurrentState);

        public void Reset()
        {
            CurrentState = InitialState;
        }

        public TOut Run(TIn input)
        {
            var (nextState, output) = CurrentState.Next(input);
            CurrentState = nextState;

            return output;
        }

        public IEnumerable<TOut> Run(IEnumerable<TIn> input)
        {
            return input.Select(Run);
        }
    }
}