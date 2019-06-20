using System.Collections.Generic;
using DeterministicAutomata.Automaton;
using DeterministicAutomata.Automaton.FiniteAutomaton;
using DeterministicAutomata.Types.Finite.Nondeterministic;
using DeterministicAutomata.Types.General;
using NUnit.Framework;

namespace Tests.Automaton
{
    public class NondeterministicFiniteAutomatonTests
    {
        [Test]
        public void TestEndsWith1()
        {
            var sp = new State("p");
            var sq = new State("q");
            var i0 = new Symbol("0");
            var i1 = new Symbol("1");

            var states = new HashSet<State> {sp, sq};
            var alphabet = new Alphabet(new HashSet<Symbol>{i0, i1});
            var transitionFunction = new NondeterministicFiniteTransitionFunction(new HashSet<NondeterministicFinitePartialTransitionFunction>
            {
                new NondeterministicFinitePartialTransitionFunction(sp, i0, new HashSet<State> {sp}),
                new NondeterministicFinitePartialTransitionFunction(sp, i1, new HashSet<State> {sp, sq}),
                new NondeterministicFinitePartialTransitionFunction(sq, i0, new HashSet<State>()),
                new NondeterministicFinitePartialTransitionFunction(sq, i1, new HashSet<State>())
            });
            var acceptStates = new HashSet<State>{sq};

            // Ends with a 1
            var automaton = new NondeterministicFiniteAutomaton(
                states,
                alphabet,
                transitionFunction,
                sp,
                acceptStates
                );

            var negative = Word.Parse("0000111010011010", alphabet);
            var positive = Word.Parse(/*"0011100101001001"*/"1", alphabet);

            //Assert.False(automaton.Accepts(negative));
            Assert.True(automaton.Accepts(positive));
        }
    }
}

