using System.Collections.Generic;

namespace IOLanguageLib.Automaton
{
    public class ContextAutomaton<TIn, TOut> : AbstractAutomaton<TIn, TOut> where TIn : IContext
    {
        public ContextAutomaton(IState<TIn, TOut> initialState, IEnumerable<IState<TIn, TOut>> finalStates) : base(
            initialState, finalStates)
        {
        }

        public IEnumerable<TOut> Run(TIn context)
        {
            do
            {
                yield return Step(context);
            } while (!context.IsOver);
        }
    }
}