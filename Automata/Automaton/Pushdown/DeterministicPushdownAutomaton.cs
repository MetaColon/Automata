using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;
using Automata.Types.Pushdown;
using Automata.Types.Pushdown.Deterministic;


namespace Automata.Automaton.Pushdown
{
    public class DeterministicPushdownAutomaton : PushdownAutomaton
    {
        public DeterministicPushdownAutomaton (HashSet <State> states, Alphabet inputAlphabet, State initialState, HashSet <State> acceptStates, DeterministicPushdownTransitionFunction transitionFunction, Symbol initialStackSymbol, Alphabet stackAlphabet)
            : base (states, inputAlphabet, initialState, acceptStates, initialStackSymbol, stackAlphabet)
            => TransitionFunction = transitionFunction;

        public DeterministicPushdownTransitionFunction TransitionFunction { get; }

        public override bool Accepts (Word word)
        {
            var passedConfigurations = new HashSet <PushdownConfiguration> ();
            var currentConfiguration = new PushdownConfiguration (InitialState, new Stack (InitialStackSymbol), word);

            // Stop when no next configuration could be found, the current configuration has read all it's input or the exact same configuration already occurred (meaning that the automaton is looping)
            while (currentConfiguration != null &&
                   !currentConfiguration.Accepted (AcceptStates) &&
                   !passedConfigurations.Any (configuration => configuration.Equals (currentConfiguration)))
            {
                passedConfigurations.Add (currentConfiguration);
                currentConfiguration = TransitionFunction.GetNextConfiguration (currentConfiguration);
            }

            return currentConfiguration?.Accepted (AcceptStates) ?? false;
        }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is DeterministicPushdownAutomaton automaton && Equals (automaton);

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                return (base.GetHashCode () * 397) ^ (TransitionFunction != null ? TransitionFunction.GetHashCode () : 0);
            }
        }

        protected bool Equals (DeterministicPushdownAutomaton other)
            => base.Equals (other) && Equals (TransitionFunction, other.TransitionFunction);
    }
}