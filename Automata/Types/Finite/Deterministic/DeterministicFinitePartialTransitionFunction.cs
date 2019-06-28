using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Finite.Deterministic
{
    public class DeterministicFinitePartialTransitionFunction : FinitePartialTransitionFunction
    {
        public DeterministicFinitePartialTransitionFunction(State inputState, Symbol inputSymbol, State resultState) : base(inputState, inputSymbol, new HashSet<State>{resultState})
        {}

        public State GetResultState()
            => ResultStates.FirstOrDefault();
    }
}