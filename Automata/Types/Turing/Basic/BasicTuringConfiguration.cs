using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Turing.Basic
{
    public class BasicTuringConfiguration
    {
        public BasicTuringConfiguration (int headIndex, Tape tape, State state, bool hasHalted = false)
        {
            HeadIndex = headIndex;
            Tape      = tape;
            State     = state;
            HasHalted = hasHalted;
        }

        public Tape  Tape      { get; }
        public int   HeadIndex { get; }
        public State State     { get; }
        public bool  HasHalted { get; }

        public bool IsAccepting (IEnumerable <State> acceptingStates)
            => acceptingStates.Any (state => state.Equals (State));

        public TapeSymbol GetCurrentSymbol ()
            => Tape.Read (HeadIndex);

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is BasicTuringConfiguration configuration && Equals (configuration);

        protected bool Equals (BasicTuringConfiguration other)
            => Equals (Tape, other.Tape) && HeadIndex == other.HeadIndex && Equals (State, other.State) && HasHalted == other.HasHalted;

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = (Tape != null ? Tape.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ HeadIndex;
                hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ HasHalted.GetHashCode ();
                return hashCode;
            }
        }

        public BasicTuringConfiguration Halt ()
            => new BasicTuringConfiguration (HeadIndex, Tape, State, true);
    }
}