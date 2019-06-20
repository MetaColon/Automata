using System.Collections.Generic;
using System.Linq;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Finite.Deterministic
{
    public class DeterministicFinitePartialTransitionFunction : FinitePartialTransitionFunction
    {
        public DeterministicFinitePartialTransitionFunction(State inputState, Symbol inputSymbol, State resultState) : base(inputState, inputSymbol, new HashSet<State>{resultState})
        {}

        public State GetResultState()
            => ResultStates.FirstOrDefault();
    }
}