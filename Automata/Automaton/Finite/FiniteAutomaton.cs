using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Automaton.Finite
{
    public abstract class FiniteAutomaton : BasicAutomaton
    {
        /// <inheritdoc />
        protected FiniteAutomaton (HashSet <State> states, Alphabet inputAlphabet, State initialState, HashSet <State> acceptStates) : base (states, inputAlphabet, initialState, acceptStates) {}
    }
}