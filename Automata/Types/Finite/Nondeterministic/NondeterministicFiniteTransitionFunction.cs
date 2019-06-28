using System.Collections.Generic;
using System.Linq;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Finite.Nondeterministic
{
    public class NondeterministicFiniteTransitionFunction : FiniteTransitionFunction
    {
        public NondeterministicFiniteTransitionFunction(HashSet<NondeterministicFinitePartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet<NondeterministicFinitePartialTransitionFunction> PartialTransitionFunctions { get; }

        public override bool Equals(object obj)
            => obj is NondeterministicFiniteTransitionFunction transitionFunction && Equals(transitionFunction);

        protected bool Equals(NondeterministicFiniteTransitionFunction other)
            => Equals(PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode() : 0;

        public HashSet<State> GetNextStates(State currentState, Symbol inputSymbol)
            => PartialTransitionFunctions.FirstOrDefault(function
                   => function.InputState.Equals(currentState) && function.InputSymbol.Equals(inputSymbol))?.GetResultStates() ?? new HashSet<State>();
    }
}