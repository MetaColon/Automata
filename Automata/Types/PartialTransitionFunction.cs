using System.Collections.Generic;

namespace Automata.Types
{
    public class PartialTransitionFunction
    {
        public PartialTransitionFunction(State inputState, InputSymbol inputSymbol, HashSet<State> resultStates)
        {
            InputState = inputState;
            InputSymbol = inputSymbol;
            ResultStates = resultStates;
        }

        public State InputState { get; }
        public InputSymbol InputSymbol { get; }

        public HashSet<State> ResultStates { get; }

        public override bool Equals(object obj)
            => obj is PartialTransitionFunction partialTransitionFunction && Equals(partialTransitionFunction);

        protected bool Equals(PartialTransitionFunction other)
            => Equals(InputState, other.InputState) && Equals(InputSymbol, other.InputSymbol) && Equals(ResultStates, other.ResultStates);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = InputState != null ? InputState.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (InputSymbol != null ? InputSymbol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ResultStates != null ? ResultStates.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}