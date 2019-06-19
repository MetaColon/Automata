namespace Automata.Types
{
    public class PartialTransitionFunction
    {
        public PartialTransitionFunction(State inputState, InputSymbol inputSymbol, State resultState)
        {
            this.InputState = inputState;
            this.InputSymbol = inputSymbol;
            this.ResultState = resultState;
        }

        public State InputState { get; }
        public InputSymbol InputSymbol { get; }

        public State ResultState { get; }

        public override bool Equals(object obj)
            => obj is PartialTransitionFunction partialTransitionFunction && Equals(partialTransitionFunction);

        protected bool Equals(PartialTransitionFunction other)
            => Equals(this.InputState, other.InputState) && Equals(this.InputSymbol, other.InputSymbol) && Equals(this.ResultState, other.ResultState);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.InputState != null ? this.InputState.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.InputSymbol != null ? this.InputSymbol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ResultState != null ? this.ResultState.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}