using System.Collections.Generic;
using System.Linq;

namespace Automata.Types
{
    public class TransitionFunction
    {
        public TransitionFunction(HashSet<PartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet<PartialTransitionFunction> PartialTransitionFunctions { get; }

        public override bool Equals(object obj)
            => obj is TransitionFunction transitionFunction && Equals(transitionFunction);

        protected bool Equals(TransitionFunction other)
            => Equals(PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode() : 0;

        public HashSet<State> GetNextStates(State currentState, InputSymbol inputSymbol)
            => PartialTransitionFunctions.FirstOrDefault(function => function.InputState.Equals(currentState) && function.InputSymbol.Equals(inputSymbol))?.ResultStates ?? new HashSet<State>();
    }
}