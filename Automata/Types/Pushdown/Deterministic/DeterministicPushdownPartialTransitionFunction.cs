using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Pushdown.Deterministic
{
    public class DeterministicPushdownPartialTransitionFunction : PushdownPartialTransitionFunction
    {
        public DeterministicPushdownPartialTransitionFunction (State inputState, Symbol inputSymbol, Symbol inputStackSymbol, (State, Symbol) resultState)
            : base (inputState, inputSymbol, inputStackSymbol, new HashSet <(State State, Symbol StackSymbol)> {resultState}) {}

        public (State State, Symbol Stacksymbol) GetResultState ()
            => ResultStates.FirstOrDefault ();
    }
}