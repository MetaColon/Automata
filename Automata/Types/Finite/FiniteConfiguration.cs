using System.Collections.Generic;
using Automata.Types;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Finite
{
    public class FiniteConfiguration : Configuration
    {
        public FiniteConfiguration(State state, Word leftSymbols) : base(state, leftSymbols) {}
    }
}