using System.Collections.Generic;
using System.Linq;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Finite.Deterministic
{
    public class DeterministicFiniteTransitionFunction : FiniteTransitionFunction
    {
        public DeterministicFiniteTransitionFunction(HashSet<DeterministicFinitePartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet<DeterministicFinitePartialTransitionFunction>
        PartialTransitionFunctions { get; }

        public override bool Equals(object obj)
            => obj is DeterministicFiniteTransitionFunction transitionFunction && Equals(transitionFunction);

        protected bool Equals(DeterministicFiniteTransitionFunction other)
            => Equals(PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode() : 0;

        public FiniteConfiguration GetNextConfiguration (FiniteConfiguration currentConfiguration)
            => new FiniteConfiguration(GetNextState(currentConfiguration.State, currentConfiguration.LeftSymbols.Peek()), currentConfiguration.LeftSymbols.SkipNext());

        private State GetNextState(State currentState, Symbol inputSymbol)
            => PartialTransitionFunctions.FirstOrDefault(function => function.InputState.Equals(currentState) && function.InputSymbol.Equals(inputSymbol))?.GetResultState();
    }
}