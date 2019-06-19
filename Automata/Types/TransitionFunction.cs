using System.Collections.Generic;
using System.Linq;

namespace Automata.Types
{
    public class TransitionFunction
    {
        public TransitionFunction(HashSet<PartialTransitionFunction> partialTransitionFunctions)
            => this.PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet<PartialTransitionFunction> PartialTransitionFunctions { get; }

        public override bool Equals(object obj)
            => obj is TransitionFunction transitionFunction && Equals(transitionFunction);

        protected bool Equals(TransitionFunction other)
            => Equals(this.PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode()
            => this.PartialTransitionFunctions != null ? this.PartialTransitionFunctions.GetHashCode() : 0;

        public State GetNextState(State currentState, InputSymbol inputSymbol)
            => this.PartialTransitionFunctions.FirstOrDefault(function => function.InputState.Equals(currentState) && function.InputSymbol.Equals(inputSymbol))?.ResultState;
    }
}