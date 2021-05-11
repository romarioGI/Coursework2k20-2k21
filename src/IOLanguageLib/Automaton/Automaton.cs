using System.Collections.Generic;
using System.Linq;

namespace IOLanguageLib.Automaton
{
    public class Automaton<TIn, TOut>
    {
        public Automaton(IState<TIn, TOut> initialState, IEnumerable<IState<TIn, TOut>> finalStates)
        {
            InitialState = initialState;
            CurrentState = InitialState;
            FinalStates = finalStates.ToHashSet();
        }

        private IState<TIn, TOut> InitialState { get; }

        private IState<TIn, TOut> CurrentState { get; set; }

        private HashSet<IState<TIn, TOut>> FinalStates { get; }

        public bool InFinalState => FinalStates.Contains(CurrentState);

        public void Reset()
        {
            CurrentState = InitialState;
        }

        public TOut Next(TIn input)
        {
            var (nextState, output) = CurrentState.Next(input);
            CurrentState = nextState;

            return output;
        }
    }
}