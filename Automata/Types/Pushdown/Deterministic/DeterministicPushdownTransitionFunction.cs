using System.Collections.Generic;
using System.Linq;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Pushdown.Deterministic
{
    public class DeterministicPushdownTransitionFunction : PushdownTransitionFunction
    {
        public HashSet<DeterministicPushdownPartialTransitionFunction> PartialTransitionFunctions { get; set; }

        public override bool Equals(object obj)
            => obj is DeterministicPushdownTransitionFunction pushdownTransitionFunction && Equals(pushdownTransitionFunction);

        protected bool Equals(DeterministicPushdownTransitionFunction other)
            => Equals(PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode() : 0;

        public (State, Symbol)? GetNextState(State currentState, Symbol readSymbol)
            => PartialTransitionFunctions.FirstOrDefault(function => function.InputState.Equals(currentState) && function.InputStackSymbol.Equals(readSymbol))?.GetResultState();
    }
}