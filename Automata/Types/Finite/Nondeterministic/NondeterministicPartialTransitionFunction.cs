using System.Collections.Generic;
using Automata.Types.General;

namespace Automata.Types.Finite.Nondeterministic
{
    public class NondeterministicPartialTransitionFunction : FinitePartialTransitionFunction
    {
        public NondeterministicPartialTransitionFunction(State inputState, InputSymbol inputSymbol, HashSet<State> resultStates) : base(inputState, inputSymbol, resultStates)
        {
        }
    }
}