using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Automaton
{
    public abstract class BasicAutomaton : Automaton
    {
        public BasicAutomaton(HashSet<State> states, Alphabet inputAlphabet, State initialState, HashSet<State> acceptStates)
        {
            States = states;
            InputAlphabet = inputAlphabet;
            InitialState = initialState;
            AcceptStates = acceptStates;
        }

        public HashSet<State> States { get; }
        public Alphabet InputAlphabet { get; }
        public State InitialState { get; }
        public HashSet<State> AcceptStates { get; }

        public override bool Equals(object obj)
            => obj is BasicAutomaton finiteAutomaton && Equals(finiteAutomaton);

        protected bool Equals(BasicAutomaton other)
            => Equals(States, other.States) &&
               Equals(InputAlphabet, other.InputAlphabet) &&
               Equals(InitialState, other.InitialState) &&
               Equals(AcceptStates, other.AcceptStates);

        public abstract bool Accepts(Word word);
        public abstract override int GetHashCode();
    }
}