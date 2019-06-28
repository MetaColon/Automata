using System.Collections.Generic;
using DeterministicAutomata.Types.General;
using DeterministicAutomata.Types.Pushdown;

namespace Automata.Types.Pushdown
{
    public class PushdownConfiguration : Configuration
    {
        public PushdownConfiguration(State state, Stack stack, Word leftSymbols) : base(state, leftSymbols)
            => Stack = stack;

        public Stack Stack { get; }
    }
}