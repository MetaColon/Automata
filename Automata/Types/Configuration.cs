using System.Collections.Generic;
using System.Linq;
using DeterministicAutomata.Types.General;

namespace Automata.Types
{
    public abstract class Configuration
    {
        protected Configuration(State state, Word leftSymbols)
        {
            State = state;
            LeftSymbols = leftSymbols;
        }

        public State State { get; }
        public Word LeftSymbols { get; }

        public bool Done()
            => LeftSymbols.Count() == 0;

        public bool Accepted(HashSet<State> acceptStates)
            => acceptStates.Any(state => state.Equals(State));
    }
}