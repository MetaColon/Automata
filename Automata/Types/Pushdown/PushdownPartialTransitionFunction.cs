using System.Collections.Generic;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Pushdown
{
    public abstract class PushdownPartialTransitionFunction
    {
        public PushdownPartialTransitionFunction(State inputState, Symbol inputSymbol, Symbol inputStackSymbol, HashSet<(State, Symbol)> resultStates)
        {
            InputState = inputState;
            InputSymbol = inputSymbol;
            ResultStates = resultStates;
            InputStackSymbol = inputStackSymbol;
        }

        public State InputState { get; }
        public Symbol InputSymbol { get; }
        public Symbol InputStackSymbol { get; }

        protected HashSet<(State, Symbol)> ResultStates { get; }

        public override bool Equals(object obj)
            => obj is PushdownPartialTransitionFunction partialTransitionFunction && Equals(partialTransitionFunction);

        protected bool Equals(PushdownPartialTransitionFunction other)
            => Equals(InputState, other.InputState) && Equals(InputSymbol, other.InputSymbol) && Equals(InputStackSymbol, other.InputStackSymbol) && Equals(ResultStates, other.ResultStates);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (InputState != null ? InputState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InputSymbol != null ? InputSymbol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InputStackSymbol != null ? InputStackSymbol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ResultStates != null ? ResultStates.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}