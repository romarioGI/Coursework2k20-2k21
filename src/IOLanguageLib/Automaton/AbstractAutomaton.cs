using System.Collections.Generic;
using System.Linq;

namespace IOLanguageLib.Automaton
{
    public abstract class AbstractAutomaton<TIn, TOut>
    {
        protected AbstractAutomaton(IState<TIn, TOut> initialState, IEnumerable<IState<TIn, TOut>> finalStates)
        {
            InitialState = initialState;
            CurrentState = InitialState;
            FinalStates = finalStates.ToHashSet();
        }

        private IState<TIn, TOut> InitialState { get; }

        private IState<TIn, TOut> CurrentState { get; set; }

        private IEnumerable<IState<TIn, TOut>> FinalStates { get; }

        public bool InFinalState => FinalStates.Contains(CurrentState);

        public void Reset()
        {
            CurrentState = InitialState;
        }

        protected TOut Step(TIn input)
        {
            var (nextState, output) = CurrentState.Next(input);
            CurrentState = nextState;

            return output;
        }
    }
}