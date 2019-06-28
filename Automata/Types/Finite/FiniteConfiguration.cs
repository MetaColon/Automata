using Automata.Types.General;


namespace Automata.Types.Finite
{
    public class FiniteConfiguration : Configuration
    {
        public FiniteConfiguration(State state, Word leftSymbols) : base(state, leftSymbols) {}
    }
}