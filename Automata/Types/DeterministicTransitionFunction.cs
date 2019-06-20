using System.Collections.Generic;
using System.Linq;

namespace Automata.Types
{
    public class DeterministicTransitionFunction
    {
        public DeterministicTransitionFunction(HashSet<DeterministicPartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet<DeterministicPartialTransitionFunction>
        PartialTransitionFunctions { get; }

        public override bool Equals(object obj)
            => obj is DeterministicTransitionFunction transitionFunction && Equals(transitionFunction);

        protected bool Equals(DeterministicTransitionFunction other)
            => Equals(PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode() : 0;

        public State GetNextState(State currentState, InputSymbol inputSymbol)
            => PartialTransitionFunctions.FirstOrDefault(function => function.InputState.Equals(currentState) && function.InputSymbol.Equals(inputSymbol))?.GetResultState();
    }
}