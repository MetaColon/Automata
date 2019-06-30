using System.Collections.Generic;
using System.Linq;

using Automata.Types.Finite;
using Automata.Types.Finite.Nondeterministic;
using Automata.Types.General;


namespace Automata.Automaton.FiniteAutomaton
{
    public class NondeterministicFiniteAutomaton : FiniteAutomaton
    {
        public NondeterministicFiniteAutomaton (HashSet <State> states, Alphabet inputAlphabet, NondeterministicFiniteTransitionFunction transitionFunction, State initialState, HashSet <State> acceptStates)
            : base (states, inputAlphabet, initialState, acceptStates)
            => TransitionFunction = transitionFunction;

        public NondeterministicFiniteTransitionFunction TransitionFunction { get; }

        public override bool Accepts (Word word)
        {
            var currentConfigurations = new HashSet <FiniteConfiguration> {new FiniteConfiguration (InitialState, word)};

            while (currentConfigurations.Count > 0 && currentConfigurations.Any (configuration => !configuration.Done ()) && currentConfigurations.All (configuration => !configuration.Accepted (AcceptStates)))
                currentConfigurations = currentConfigurations.SelectMany (configuration => TransitionFunction.GetNextConfigurations (configuration)).ToHashSet ();

            return currentConfigurations.Any (configuration => configuration.Accepted (AcceptStates));
        }

        public override bool Equals (object obj)
            => obj is NondeterministicFiniteAutomaton finiteAutomaton && Equals (finiteAutomaton);

        protected bool Equals (NondeterministicFiniteAutomaton other)
            => Equals (TransitionFunction, other.TransitionFunction) && base.Equals (other);

        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = States != null ? States.GetHashCode () : 0;
                hashCode = (hashCode * 397) ^ (InputAlphabet != null ? InputAlphabet.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (TransitionFunction != null ? TransitionFunction.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (InitialState != null ? InitialState.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (AcceptStates != null ? AcceptStates.GetHashCode () : 0);
                return hashCode;
            }
        }
    }
}