using System.Collections.Generic;
using System.Linq;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Pushdown.Deterministic
{
    public class DeterministicPushdownPartialTransitionFunction : PushdownPartialTransitionFunction
    {
        public DeterministicPushdownPartialTransitionFunction(State inputState, Symbol inputSymbol, Symbol inputStackSymbol, HashSet<(State, Symbol)> resultStates)
            : base(inputState, inputSymbol, inputStackSymbol, resultStates)
        {
        }

        public (State, Symbol) GetResultState()
            => ResultStates.FirstOrDefault();
    }
}