using System.Collections.Generic;
using Automata.Automaton;
using Automata.Types;
using NUnit.Framework;

namespace Tests.Automaton
{
    public class DeterministicFiniteAutomatonTests
    {
        [Test]
        public void PositiveTest()
        {
            State s1 = new State("S1");
            State s2 = new State("S2");
            InputSymbol i0 = new InputSymbol("0");
            InputSymbol i1 = new InputSymbol("1");

            HashSet<State> states = new HashSet<State> {s1, s2};
            Alphabet alphabet = new Alphabet(new HashSet<InputSymbol> {i0, i1});
            TransitionFunction transitionFunction = new TransitionFunction(new HashSet<PartialTransitionFunction>
            {
                new PartialTransitionFunction(s1, i0, s2),
                new PartialTransitionFunction(s1, i1, s1),
                new PartialTransitionFunction(s2, i0, s1),
                new PartialTransitionFunction(s2, i1, s2)
            });
            HashSet<State> acceptStates = new HashSet<State> {s1};

            // Even number of zeros
            DeterministicFiniteAutomaton automaton = new DeterministicFiniteAutomaton(
                states,
                alphabet,
                transitionFunction,
                s1,
                acceptStates
                );


            Word negative = Word.Parse("0101010001001101", alphabet);
            Word positive = Word.Parse("010011101010", alphabet);


            bool negativeResult = automaton.Accepts(negative);
            bool positiveResult = automaton.Accepts(positive);

            Assert.False(negativeResult, "The automaton was not expected to accept an odd number of zeros");
            Assert.True(positiveResult, "The automaton was expected to accept an even number of zeros");
        }
    }
}