using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Types.Finite
{
    public abstract class FinitePartialTransitionFunction
    {
        public FinitePartialTransitionFunction(State inputState, Symbol inputSymbol, HashSet<State> resultStates)
        {
            InputState = inputState;
            InputSymbol = inputSymbol;
            ResultStates = resultStates;
        }

        public State InputState { get; }
        public Symbol InputSymbol { get; }

        protected HashSet<State> ResultStates { get; }

        public override bool Equals(object obj)
            => obj is FinitePartialTransitionFunction partialTransitionFunction && Equals(partialTransitionFunction);

        protected bool Equals(FinitePartialTransitionFunction other)
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