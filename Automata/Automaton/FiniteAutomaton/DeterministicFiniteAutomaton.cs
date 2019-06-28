using System.Collections.Generic;
using System.Linq;
using Automata.Types;
using DeterministicAutomata.Types.Finite;
using DeterministicAutomata.Types.Finite.Deterministic;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Automaton.FiniteAutomaton
{
    public class DeterministicFiniteAutomaton : BasicAutomaton
    {
        public DeterministicFiniteAutomaton(HashSet<State> states, Alphabet inputAlphabet, DeterministicFiniteTransitionFunction transitionFunction, State initialState, HashSet<State> acceptStates)
            : base(states, inputAlphabet, initialState, acceptStates)
            => TransitionFunction = transitionFunction;

        public DeterministicFiniteTransitionFunction TransitionFunction { get; }

        public override bool Accepts(Word word)
        {
            var currentConfiguration = new FiniteConfiguration(InitialState, word);

            while (currentConfiguration != null && !currentConfiguration.Done())
                currentConfiguration = TransitionFunction.GetNextConfiguration(currentConfiguration);

            return currentConfiguration != null && currentConfiguration.Accepted(AcceptStates);
        }

        public override bool Equals(object obj)
            => obj is DeterministicFiniteAutomaton finiteAutomaton && Equals(finiteAutomaton);

        protected bool Equals(DeterministicFiniteAutomaton other)
            => Equals(TransitionFunction, other.TransitionFunction) && base.Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = States != null ? States.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (InputAlphabet != null ? InputAlphabet.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TransitionFunction != null ? TransitionFunction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InitialState != null ? InitialState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AcceptStates != null ? AcceptStates.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}