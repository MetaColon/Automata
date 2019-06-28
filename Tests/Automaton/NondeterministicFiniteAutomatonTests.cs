using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Automaton.FiniteAutomaton;
using Automata.Types.Finite.Nondeterministic;
using Automata.Types.General;

using NUnit.Framework;


namespace Tests.Automaton
{
    public class NondeterministicFiniteAutomatonTests
    {
        private Random Random = new Random ();

        [Test]
        public void TestEndsWith1 ()
        {
            var sp = new State ("p");
            var sq = new State ("q");
            var i0 = new Symbol ("0");
            var i1 = new Symbol ("1");

            var states   = new HashSet <State> {sp, sq};
            var alphabet = new Alphabet (new HashSet <Symbol> {i0, i1});
            var transitionFunction = new NondeterministicFiniteTransitionFunction (new HashSet <NondeterministicFinitePartialTransitionFunction>
            {
                new NondeterministicFinitePartialTransitionFunction (sp, i0, new HashSet <State> {sp}),
                new NondeterministicFinitePartialTransitionFunction (sp, i1, new HashSet <State> {sp, sq}),
                new NondeterministicFinitePartialTransitionFunction (sq, i0, new HashSet <State> ()),
                new NondeterministicFinitePartialTransitionFunction (sq, i1, new HashSet <State> ())
            });
            var acceptStates = new HashSet <State> {sq};

            // Ends with a 1
            var automaton = new NondeterministicFiniteAutomaton (
                states,
                alphabet,
                transitionFunction,
                sp,
                acceptStates
            );

            var negative = Word.Parse ("0000111010011010", alphabet);
            var positive = Word.Parse ("0011100101001001", alphabet);

            Assert.False (automaton.Accepts (negative));
            Assert.True (automaton.Accepts (positive));

            for (var i = 0; i < 10; i++)
            {
                var randomNegative = Word.Parse (string.Join ("", Enumerable.Range (1, Random.Next (20)).Select (_ => Random.Next (2))) + "0", alphabet);
                var randomPositive = Word.Parse (string.Join ("", Enumerable.Range (1, Random.Next (20)).Select (_ => Random.Next (2))) + "1", alphabet);
                Assert.False (automaton.Accepts (randomNegative));
                Assert.True (automaton.Accepts (randomPositive));
            }
        }

        [Test]
        public void Test2Or3 ()
        {
            var s0 = new State ("0");
            var s1 = new State ("1");
            var s2 = new State ("2");
            var s3 = new State ("3");
            var s4 = new State ("4");
            var s5 = new State ("5");

            var i0 = new Symbol ("0");

            var states       = new HashSet <State> {s0, s1, s2, s3, s4, s5};
            var initialState = s0;
            var alphabet     = new Alphabet (new HashSet <Symbol> {i0});
            var acceptStates = new HashSet <State> {s1, s3};
            var transitionFunction = new NondeterministicFiniteTransitionFunction (new HashSet <NondeterministicFinitePartialTransitionFunction>
            {
                new NondeterministicFinitePartialTransitionFunction (s0, Symbol.EPSILON, new HashSet <State> {s1, s3}),
                new NondeterministicFinitePartialTransitionFunction (s1, i0, new HashSet <State> {s2}),
                new NondeterministicFinitePartialTransitionFunction (s2, i0, new HashSet <State> {s1}),
                new NondeterministicFinitePartialTransitionFunction (s3, i0, new HashSet <State> {s4}),
                new NondeterministicFinitePartialTransitionFunction (s4, i0, new HashSet <State> {s5}),
                new NondeterministicFinitePartialTransitionFunction (s5, i0, new HashSet <State> {s3})
            });

            // Contains 2 or 3 zeros
            var automaton = new NondeterministicFiniteAutomaton (
                states,
                alphabet,
                transitionFunction,
                initialState,
                acceptStates);

            var negative = Word.Parse ("00000", alphabet);
            var positive = Word.Parse ("000", alphabet);

            Assert.False (automaton.Accepts (negative));
            Assert.True (automaton.Accepts (positive));

            for (var i = 0; i < 10; i++)
            {
                var unknown = Word.Parse (string.Join ("", Enumerable.Repeat (0, Random.Next (5, 20))), alphabet);

                if (unknown.Count () % 3 == 0 || unknown.Count () % 2 == 0)
                    Assert.True (automaton.Accepts (unknown));
                else
                    Assert.False (automaton.Accepts (unknown));
            }
        }
    }
}