using System;

using Automata.Types.General;


namespace Automata.Types.Turing.Basic
{
    public class BasicTuringPartialTransitionFunction
    {
        /// <inheritdoc />
        public BasicTuringPartialTransitionFunction (State inputState, TapeSymbol inputSymbol, State resultState, TapeSymbol resultSymbol, MovementDirection movementDirection)
        {
            InputState        = inputState;
            InputSymbol       = inputSymbol;
            ResultState       = resultState;
            ResultSymbol      = resultSymbol;
            MovementDirection = movementDirection;
        }

        public State      InputState  { get; }
        public TapeSymbol InputSymbol { get; }

        public State             ResultState       { get; }
        public TapeSymbol        ResultSymbol      { get; }
        public MovementDirection MovementDirection { get; }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is BasicTuringPartialTransitionFunction function && Equals (function);

        protected bool Equals (BasicTuringPartialTransitionFunction other)
            => Equals (InputState, other.InputState) && Equals (InputSymbol, other.InputSymbol) && Equals (ResultState, other.ResultState) && Equals (ResultSymbol, other.ResultSymbol) && MovementDirection == other.MovementDirection;

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = InputState != null ? InputState.GetHashCode () : 0;
                hashCode = (hashCode * 397) ^ (InputSymbol != null ? InputSymbol.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (ResultState != null ? ResultState.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (ResultSymbol != null ? ResultSymbol.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (int) MovementDirection;
                return hashCode;
            }
        }

        public int GetNewHeadIndex (int currentHeadIndex)
        {
            switch (MovementDirection)
            {
                case MovementDirection.Left:
                    return currentHeadIndex <= 0 ? 0 : currentHeadIndex - 1;
                case MovementDirection.Right:
                    return currentHeadIndex + 1;
                default:
                    throw new IndexOutOfRangeException ("Unknown movement direction");
            }
        }
    }
}