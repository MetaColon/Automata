using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;
using Automata.Types.Pushdown;
using Automata.Types.Pushdown.Deterministic;


namespace Automata.Automaton.Pushdown
{
    public class DeterministicPushdownAutomaton : BasicAutomaton
    {
        public DeterministicPushdownAutomaton (HashSet <State> states, Alphabet inputAlphabet, State initialState, HashSet <State> acceptStates, DeterministicPushdownTransitionFunction transitionFunction, Symbol initialStackSymbol, Alphabet stackAlphabet)
            : base (states, inputAlphabet, initialState, acceptStates)
        {
            TransitionFunction = transitionFunction;
            InitialStackSymbol = initialStackSymbol;
            StackAlphabet      = stackAlphabet;
        }

        public DeterministicPushdownTransitionFunction TransitionFunction { get; }
        public Symbol                                  InitialStackSymbol { get; }
        public Alphabet                                StackAlphabet      { get; }

        public override bool Accepts (Word word)
        {
            var passedConfigurations = new HashSet <PushdownConfiguration> ();
            var currentConfiguration = new PushdownConfiguration (InitialState, new Stack (InitialStackSymbol), word);

            // Stop when no next configuration could be found, the current configuration has read all it's input or the exact same configuration already occurred (meaning that the automaton is looping)
            while (currentConfiguration != null &&
                   (!currentConfiguration.Done () || !currentConfiguration.Accepted (AcceptStates)) &&
                   !passedConfigurations.Any (configuration => configuration.Equals (currentConfiguration)))
            {
                passedConfigurations.Add (currentConfiguration);
                currentConfiguration = TransitionFunction.GetNextConfiguration (currentConfiguration);
            }

            return (currentConfiguration?.Done () ?? false) && currentConfiguration.Accepted (AcceptStates);
        }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is DeterministicPushdownAutomaton automaton && Equals (automaton);

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = (TransitionFunction != null ? TransitionFunction.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (InitialStackSymbol != null ? InitialStackSymbol.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (StackAlphabet != null ? StackAlphabet.GetHashCode () : 0);
                return hashCode;
            }
        }

        protected bool Equals (DeterministicPushdownAutomaton other)
            => base.Equals (other) && Equals (TransitionFunction, other.TransitionFunction) && Equals (InitialStackSymbol, other.InitialStackSymbol) && Equals (StackAlphabet, other.StackAlphabet);
    }
}