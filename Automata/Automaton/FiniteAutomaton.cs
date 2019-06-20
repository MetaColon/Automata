using System.Collections.Generic;
using System.Linq;
using Automata.Types;

namespace Automata.Automaton
{
    public class FiniteAutomaton
    {
        public FiniteAutomaton(HashSet<State> states, Alphabet inputAlphabet, TransitionFunction transitionFunction, State initialState, HashSet<State> acceptStates)
        {
            States = states;
            InputAlphabet = inputAlphabet;
            TransitionFunction = transitionFunction;
            InitialState = initialState;
            AcceptStates = acceptStates;
        }

        public HashSet<State> States { get; }
        public Alphabet InputAlphabet { get; }
        public TransitionFunction TransitionFunction { get; }
        public State InitialState { get; }
        public HashSet<State> AcceptStates { get; }

        public override bool Equals(object obj)
            => obj is FiniteAutomaton finiteAutomaton && Equals(finiteAutomaton);

        protected bool Equals(FiniteAutomaton other)
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
            var currentStates = new HashSet<State> {InitialState};
            foreach (var inputSymbol in word.InputSymbols)
            {
                var newStates = new HashSet<State>();
                foreach (var state in currentStates)
                {
                    var resultStates = TransitionFunction.GetNextStates(state, inputSymbol);
                    foreach (var resultState in resultStates)
                        newStates.Add(resultState);
                }
            }

            return AcceptStates.Any(accept => currentStates.Any(accept.Equals));
        }
    }
}