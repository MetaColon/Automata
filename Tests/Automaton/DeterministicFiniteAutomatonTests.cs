using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Automaton.FiniteAutomaton;
using Automata.Types.Finite.Deterministic;
using Automata.Types.General;

using NUnit.Framework;


namespace Tests.Automaton
{
    public class DeterministicFiniteAutomatonTests
    {
        private Random Random = new Random ();

        [Test]
        public void TestEvenExample ()
        {
            var s1 = new State ("S1");
            var s2 = new State ("S2");
            var i0 = new Symbol ("0");
            var i1 = new Symbol ("1");

            var states       = new HashSet <State> {s1, s2};
            var alphabet     = new Alphabet (new HashSet <Symbol> {i0, i1});
            var acceptStates = new HashSet <State> {s1};
            var transitionFunction = new DeterministicFiniteTransitionFunction (new HashSet <DeterministicFinitePartialTransitionFunction>
            {
                new DeterministicFinitePartialTransitionFunction (s1, i0, s2),
                new DeterministicFinitePartialTransitionFunction (s1, i1, s1),
                new DeterministicFinitePartialTransitionFunction (s2, i0, s1),
                new DeterministicFinitePartialTransitionFunction (s2, i1, s2)
            });

            // Even number of zeros
            var automaton = new DeterministicFiniteAutomaton (
                states,
                alphabet,
                transitionFunction,
                s1,
                acceptStates
            );


            var negative = Word.Parse ("0101010001001101", alphabet);
            var positive = Word.Parse ("010011101010", alphabet);

            Assert.False (automaton.Accepts (negative), "The automaton was not expected to accept an odd number of zeros");
            Assert.True (automaton.Accepts (positive), "The automaton was expected to accept an even number of zeros");

            for (var i = 0; i < 10; i++)
            {
                var unknown = Word.Parse (string.Join ("", Enumerable.Range (1, Random.Next (20)).Select (_ => Random.Next (2))), alphabet);

                if (unknown.InputSymbols.Count (symbol => symbol.Value.Equals ("0")) % 2 == 0)
                    Assert.True (automaton.Accepts (unknown));
                else
                    Assert.False (automaton.Accepts (unknown));
            }
        }

        [Test]
        public void TestEnsWithStart ()
        {
            var s  = new State ("s");
            var q1 = new State ("q1");
            var q2 = new State ("q2");
            var r1 = new State ("r1");
            var r2 = new State ("r2");

            var a = new Symbol ("a");
            var b = new Symbol ("b");

            var states       = new HashSet <State> {s, q1, q2, r1, r2};
            var alphabet     = new Alphabet (new HashSet <Symbol> {a, b});
            var acceptStates = new HashSet <State> {q1, r1};
            var transitionFunction = new DeterministicFiniteTransitionFunction (new HashSet <DeterministicFinitePartialTransitionFunction>
            {
                new DeterministicFinitePartialTransitionFunction (s, a, q1),
                new DeterministicFinitePartialTransitionFunction (s, b, r1),
                new DeterministicFinitePartialTransitionFunction (q1, a, q1),
                new DeterministicFinitePartialTransitionFunction (q1, b, q2),
                new DeterministicFinitePartialTransitionFunction (q2, b, q2),
                new DeterministicFinitePartialTransitionFunction (q2, a, q1),
                new DeterministicFinitePartialTransitionFunction (r1, b, r1),
                new DeterministicFinitePartialTransitionFunction (r1, a, r2),
                new DeterministicFinitePartialTransitionFunction (r2, a, r2),
                new DeterministicFinitePartialTransitionFunction (r2, b, r1)
            });

            // Ends with the same symbol as it started
            var automaton = new DeterministicFiniteAutomaton (
                states,
                alphabet,
                transitionFunction,
                s,
                acceptStates);


            var negative = Word.Parse ("ababbabbab", alphabet);
            var positive = Word.Parse ("ababbabbaa", alphabet);

            Assert.False (automaton.Accepts (negative));
            Assert.True (automaton.Accepts (positive));

            for (var i = 0; i < 10; i++)
            {
                var unknown = Word.Parse (string.Join ("", Enumerable.Range (1, Random.Next (20)).Select (_ => Random.Next (2) == 1 ? "b" : "a")), alphabet);

                if (unknown.Count () > 0 && unknown.InputSymbols.Peek ().Equals (unknown.InputSymbols.ToArray () [unknown.Count () - 1]))
                    Assert.True (automaton.Accepts (unknown));
                else
                    Assert.False (automaton.Accepts (unknown));
            }
        }
    }
}