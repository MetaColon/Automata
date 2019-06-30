using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Types.Pushdown.Nondeterministic
{
    public class NondeterministicPushdownPartialTransitionFunction : PushdownPartialTransitionFunction
    {
        /// <inheritdoc />
        public NondeterministicPushdownPartialTransitionFunction (State inputState, Symbol inputSymbol, Symbol inputStackSymbol, HashSet <(State, Symbol)> resultStates)
            : base (inputState, inputSymbol, inputStackSymbol, resultStates) {}

        public HashSet <(State State, Symbol StackSymbol)> GetResultStates ()
            => ResultStates;
    }
}