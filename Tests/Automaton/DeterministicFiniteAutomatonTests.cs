using System.Collections.Generic;
using DeterministicAutomata.Automaton;
using DeterministicAutomata.Automaton.FiniteAutomaton;
using DeterministicAutomata.Types;
using DeterministicAutomata.Types.Finite.Deterministic;
using DeterministicAutomata.Types.General;
using NUnit.Framework;

namespace Tests.Automaton
{
    public class DeterministicFiniteAutomatonTests
    {
        [Test]
        public void TestEvenExample()
        {
            var s1 = new State("S1");
            var s2 = new State("S2");
            var i0 = new Symbol("0");
            var i1 = new Symbol("1");

            var states = new HashSet<State> {s1, s2};
            var alphabet = new Alphabet(new HashSet<Symbol> {i0, i1});
            var transitionFunction = new DeterministicFiniteTransitionFunction(new HashSet<DeterministicFinitePartialTransitionFunction>
            {
                new DeterministicFinitePartialTransitionFunction(s1, i0, s2),
                new DeterministicFinitePartialTransitionFunction(s1, i1, s1),
                new DeterministicFinitePartialTransitionFunction(s2, i0, s1),
                new DeterministicFinitePartialTransitionFunction(s2, i1, s2)
            });
            var acceptStates = new HashSet<State> {s1};

            // Even number of zeros
            var automaton = new DeterministicFiniteAutomaton(
                states,
                alphabet,
                transitionFunction,
                s1,
                acceptStates
                );


            var negative = Word.Parse("0101010001001101", alphabet);
            var positive = Word.Parse("010011101010", alphabet);


            var negativeResult = automaton.Accepts(negative);
            var positiveResult = automaton.Accepts(positive);

            Assert.False(negativeResult, "The automaton was not expected to accept an odd number of zeros");
            Assert.True(positiveResult, "The automaton was expected to accept an even number of zeros");
        }
    }
}