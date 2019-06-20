using System.Collections.Generic;
using System.Linq;
using Automata.Types;

namespace Automata.Automaton
{
    public class DeterministicFiniteAutomaton
    {
        public DeterministicFiniteAutomaton(HashSet<State> states, Alphabet inputAlphabet, DeterministicTransitionFunction transitionFunction, State initialState, HashSet<State> acceptStates)
        {
            States = states;
            InputAlphabet = inputAlphabet;
            TransitionFunction = transitionFunction;
            InitialState = initialState;
            AcceptStates = acceptStates;
        }

        public HashSet<State> States { get; }
        public Alphabet InputAlphabet { get; }
        public DeterministicTransitionFunction TransitionFunction { get; }
        public State InitialState { get; }
        public HashSet<State> AcceptStates { get; }

        public override bool Equals(object obj)
            => obj is DeterministicFiniteAutomaton deterministicFiniteAutomaton && Equals(deterministicFiniteAutomaton);

        protected bool Equals(DeterministicFiniteAutomaton other)
            => Equals(States, other.States) &&
               Equals(InputAlphabet, other.InputAlphabet) &&
               Equals(TransitionFunction, other.TransitionFunction) &&
               Equals(InitialState, other.InitialState) &&
               Equals(AcceptStates, other.AcceptStates);

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

        public bool Accepts(Word word)
        {
            var currentState = InitialState;
            foreach (var inputSymbol in word.InputSymbols)
            {
                if (currentState == null)
                    return false;

                currentState = TransitionFunction.GetNextState(currentState, inputSymbol);
            }

            return AcceptStates.Any(state => state.Equals(currentState));
        }
    }
}