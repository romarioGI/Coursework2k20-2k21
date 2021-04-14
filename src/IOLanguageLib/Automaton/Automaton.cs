using System.Collections.Generic;
using System.Linq;

namespace IOLanguageLib.Automaton
{
    public class Automaton<TIn, TOut>: AbstractAutomaton<TIn, TOut>
    {
        public Automaton(IState<TIn, TOut> initialState, IEnumerable<IState<TIn, TOut>> finalStates) : base(initialState, finalStates)
        {
        }

        public IEnumerable<TOut> Run(IEnumerable<TIn> input)
        {
            return input.Select(Step);
        }
    }
}