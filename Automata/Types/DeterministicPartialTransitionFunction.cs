using System.Collections.Generic;
using System.Linq;

namespace Automata.Types
{
    public class DeterministicPartialTransitionFunction : PartialTransitionFunction
    {
        public DeterministicPartialTransitionFunction(State inputState, InputSymbol inputSymbol, State resultState) : base(inputState, inputSymbol, new HashSet<State>{resultState})
        {}

        public State GetResultState()
            => ResultStates.FirstOrDefault();
    }
}