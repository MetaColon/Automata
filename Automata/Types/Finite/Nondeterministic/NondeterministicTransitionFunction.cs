using System.Collections.Generic;
using System.Linq;
using Automata.Types.General;

namespace Automata.Types.Finite.Nondeterministic
{
    public class NondeterministicTransitionFunction : FiniteTransitionFunction
    {
        public NondeterministicTransitionFunction(HashSet<NondeterministicPartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet<NondeterministicPartialTransitionFunction> PartialTransitionFunctions { get; }

        public override bool Equals(object obj)
            => obj is NondeterministicTransitionFunction transitionFunction && Equals(transitionFunction);

        protected bool Equals(NondeterministicTransitionFunction other)
            => Equals(PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode() : 0;

        public HashSet<State> GetNextStates(State currentState, InputSymbol inputSymbol)
            => PartialTransitionFunctions.FirstOrDefault(function => function.InputState.Equals(currentState) && function.InputSymbol.Equals(inputSymbol))?.ResultStates ?? new HashSet<State>();
    }
}