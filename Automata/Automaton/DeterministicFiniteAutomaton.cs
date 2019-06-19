using System.Collections.Generic;
using System.Linq;
using Automata.Types;

namespace Automata.Automaton
{
    public class DeterministicFiniteAutomaton
    {
        public DeterministicFiniteAutomaton(HashSet<State> states, Alphabet inputAlphabet, TransitionFunction transitionFunction, State initialState, HashSet<State> acceptStates)
        {
            this.States = states;
            this.InputAlphabet = inputAlphabet;
            this.TransitionFunction = transitionFunction;
            this.InitialState = initialState;
            this.AcceptStates = acceptStates;
        }

        public HashSet<State> States { get; }
        public Alphabet InputAlphabet { get; }
        public TransitionFunction TransitionFunction { get; }
        public State InitialState { get; }
        public HashSet<State> AcceptStates { get; }

        public override bool Equals(object obj)
            => obj is DeterministicFiniteAutomaton deterministicFiniteAutomaton && Equals(deterministicFiniteAutomaton);

        protected bool Equals(DeterministicFiniteAutomaton other)
            => Equals(this.States, other.States) && Equals(this.InputAlphabet, other.InputAlphabet) && Equals(this.TransitionFunction, other.TransitionFunction) && Equals(this.InitialState, other.InitialState) && Equals(this.AcceptStates, other.AcceptStates);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.States != null ? this.States.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.InputAlphabet != null ? this.InputAlphabet.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TransitionFunction != null ? this.TransitionFunction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.InitialState != null ? this.InitialState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptStates != null ? this.AcceptStates.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool Accepts(Word word)
        {
            State currentState = this.InitialState;
            foreach (InputSymbol inputSymbol in word.InputSymbols)
            {
                if (currentState == null)
                    return false;

                currentState = this.TransitionFunction.GetNextState(currentState, inputSymbol);
            }

            return this.AcceptStates.Any(state => state.Equals(currentState));
        }
    }
}