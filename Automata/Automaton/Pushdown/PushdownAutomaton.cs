using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Automaton.Pushdown
{
    public abstract class PushdownAutomaton : BasicAutomaton
    {
        /// <inheritdoc />
        protected PushdownAutomaton (HashSet <State> states, Alphabet inputAlphabet, State initialState, HashSet <State> acceptStates, Symbol initialStackSymbol, Alphabet stackAlphabet)
            : base (states, inputAlphabet, initialState, acceptStates)
        {
            InitialStackSymbol = initialStackSymbol;
            StackAlphabet      = stackAlphabet;
        }

        public Symbol   InitialStackSymbol { get; }
        public Alphabet StackAlphabet      { get; }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is PushdownAutomaton automaton && Equals (automaton);

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = base.GetHashCode ();
                hashCode = (hashCode * 397) ^ (InitialStackSymbol != null ? InitialStackSymbol.GetHashCode () : 0);
                hashCode = (hashCode * 397) ^ (StackAlphabet != null ? StackAlphabet.GetHashCode () : 0);
                return hashCode;
            }
        }

        protected bool Equals (PushdownAutomaton other)
            => base.Equals (other) && Equals (InitialStackSymbol, other.InitialStackSymbol) && Equals (StackAlphabet, other.StackAlphabet);
    }
}