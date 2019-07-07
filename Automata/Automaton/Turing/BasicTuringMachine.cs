using System.Collections.Generic;
using System.Data;
using System.Linq;

using Automata.Types.General;
using Automata.Types.Turing;
using Automata.Types.Turing.Basic;


namespace Automata.Automaton.Turing
{
    public class BasicTuringMachine : BasicAutomaton
    {
        /// <inheritdoc />
        public BasicTuringMachine (HashSet <State> states, Alphabet inputAlphabet, State initialState, HashSet <State> acceptStates, BasicTuringTransitionFunction transitionFunction, TapeAlphabet tapeAlphabet) : base (states, inputAlphabet, initialState, acceptStates)
        {
            TransitionFunction = transitionFunction;
            TapeAlphabet       = tapeAlphabet;

            if (inputAlphabet.InputSymbols.Any (symbol => TapeAlphabet.TapeSymbols.All (tapeSymbol => !tapeSymbol.Value.Equals (symbol.Value))))
                throw new InvalidConstraintException ("The tape alphabet must include the input alphabet.");
        }

        public TapeAlphabet                  TapeAlphabet       { get; }
        public BasicTuringTransitionFunction TransitionFunction { get; }

        /// <inheritdoc />
        public override bool Accepts (Word word)
        {
            var currentConfiguration = new BasicTuringConfiguration (0, new Tape (word), InitialState);

            do
                currentConfiguration = TransitionFunction.GetNextConfiguration (currentConfiguration);
            while (!currentConfiguration.HasHalted);

            return currentConfiguration.IsAccepting (AcceptStates);
        }
    }
}