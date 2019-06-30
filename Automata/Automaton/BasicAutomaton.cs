using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Automaton
{
    public abstract class BasicAutomaton : Automaton
    {
        public BasicAutomaton (HashSet <State> states, Alphabet inputAlphabet, State initialState, HashSet <State> acceptStates)
        {
            States        = states;
            InputAlphabet = inputAlphabet;
            InitialState  = initialState;
            AcceptStates  = acceptStates;
        }

        public HashSet <State> States        { get; }
        public Alphabet        InputAlphabet { get; }
        public State           InitialState  { get; }
        public HashSet <State> AcceptStates  { get; }

        public override bool Equals (object obj)
            => obj is BasicAutomaton finiteAutomaton && Equals (finiteAutomaton);

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = (States != null ? States.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (InputAlphabet != null ? InputAlphabet.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (InitialState != null ? InitialState.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (AcceptStates != null ? AcceptStates.GetHashCode () : 0);
                return hashCode;
            }
        }

        protected bool Equals (BasicAutomaton other)
            => Equals (States, other.States) &&
               Equals (InputAlphabet, other.InputAlphabet) &&
               Equals (InitialState, other.InitialState) &&
               Equals (AcceptStates, other.AcceptStates);

        public abstract          bool Accepts (Word word);
    }
}