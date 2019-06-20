using System.Collections.Generic;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Types.Finite.Nondeterministic
{
    public class NondeterministicFinitePartialTransitionFunction : FinitePartialTransitionFunction
    {
        public NondeterministicFinitePartialTransitionFunction(State inputState, Symbol inputSymbol, HashSet<State> resultStates) : base(inputState, inputSymbol, resultStates)
        {
        }

        public HashSet<State> GetResultStates()
            => ResultStates;
    }
}