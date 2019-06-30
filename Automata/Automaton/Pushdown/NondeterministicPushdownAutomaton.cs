using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;
using Automata.Types.Pushdown;
using Automata.Types.Pushdown.Nondeterministic;


namespace Automata.Automaton.Pushdown
{
    public class NondeterministicPushdownAutomaton : PushdownAutomaton
    {
        /// <inheritdoc />
        public NondeterministicPushdownAutomaton (HashSet <State> states, Alphabet inputAlphabet, State initialState, HashSet <State> acceptStates, Symbol initialStackSymbol, Alphabet stackAlphabet, NondeterministicPushdownTransitionFunction transitionFunction)
            : base (states, inputAlphabet, initialState, acceptStates, initialStackSymbol, stackAlphabet)
            => TransitionFunction = transitionFunction;

        public NondeterministicPushdownTransitionFunction TransitionFunction { get; }

        /// <inheritdoc />
        public override bool Accepts (Word word)
        {
            var passedConfigurations  = new HashSet <PushdownConfiguration> ();
            var currentConfigurations = new HashSet <PushdownConfiguration> {new PushdownConfiguration (InitialState, new Stack (InitialStackSymbol), word)};

            while (currentConfigurations.Count > 0 &&
                   currentConfigurations.All (configuration => !configuration.Accepted (AcceptStates)))
            {
                foreach (var currentConfiguration in currentConfigurations)
                    passedConfigurations.Add (currentConfiguration);

                currentConfigurations = TransitionFunction.GetNextConfigurations (currentConfigurations).Except (passedConfigurations).ToHashSet ();
            }

            return currentConfigurations.Any (configuration => configuration.Accepted (AcceptStates));
        }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is NondeterministicPushdownAutomaton automaton && Equals (automaton);

        protected bool Equals (NondeterministicPushdownAutomaton other)
            => base.Equals (other) && TransitionFunction.Equals (other.TransitionFunction);

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                return (base.GetHashCode () * 397) ^ TransitionFunction.GetHashCode ();
            }
        }
    }
}