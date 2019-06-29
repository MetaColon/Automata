using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types
{
    public abstract class Configuration
    {
        protected Configuration (State state, Word leftSymbols)
        {
            State       = state;
            LeftSymbols = leftSymbols;
        }

        public State State       { get; }
        public Word  LeftSymbols { get; }

        public bool Done ()
            => LeftSymbols.Count () == 0;

        public bool Accepted (HashSet <State> acceptStates)
            => Done () && acceptStates.Any (state => state.Equals (State));

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is Configuration configuration && Equals (configuration);

        protected bool Equals (Configuration other)
            => Equals (State, other.State) && Equals (LeftSymbols, other.LeftSymbols);

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                return ((State != null ? State.GetHashCode () : 0) * 397) ^ (LeftSymbols != null ? LeftSymbols.GetHashCode () : 0);
            }
        }
    }
}