using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Automaton.Pushdown;
using Automata.Types.General;
using Automata.Types.Pushdown.Deterministic;

using NUnit.Framework;


namespace Tests.Automaton
{
    public class DeterministicPushdownAutomatonTests
    {
        private Random Random = new Random ();

        [Test]
        public void Test0ToN1ToN ()
        {
            var q1           = new State ("q1");
            var q2           = new State ("q2");
            var q3           = new State ("q3");
            var q4           = new State ("q4");
            var qr           = new State ("qr");
            var states       = new HashSet <State> {q1, q2, q3, q4};
            var acceptStates = new HashSet <State> {q1, q4};

            var i0       = new Symbol ("0");
            var i1       = new Symbol ("1");
            var symbols  = new HashSet <Symbol> {i0, i1};
            var alphabet = new Alphabet (symbols);

            var s0            = new Symbol ("0");
            var se            = new Symbol ("$");
            var stackSymbols  = new HashSet <Symbol> {s0, se};
            var stackAlphabet = new Alphabet (stackSymbols);

            var transitionFunction = new DeterministicPushdownTransitionFunction (new HashSet <DeterministicPushdownPartialTransitionFunction>
            {
                // Reject state
                new DeterministicPushdownPartialTransitionFunction (qr, i0, s0, (qr, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (qr, i0, se, (qr, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (qr, i1, s0, (qr, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (qr, i1, se, (qr, Symbol.EPSILON)),
                // Usual states
                new DeterministicPushdownPartialTransitionFunction (q1, Symbol.EPSILON, Symbol.EPSILON, (q2, se)),
                new DeterministicPushdownPartialTransitionFunction (q2, i0, Symbol.EPSILON, (q2, s0)),
                new DeterministicPushdownPartialTransitionFunction (q2, i1, s0, (q3, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (q2, i1, se, (qr, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (q3, i0, s0, (qr, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (q3, i1, s0, (q3, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (q3, Symbol.EPSILON, se, (q4, Symbol.EPSILON)),
                new DeterministicPushdownPartialTransitionFunction (q4, Symbol.EPSILON, Symbol.EPSILON, (qr, Symbol.EPSILON)),
            });

            var automaton = new DeterministicPushdownAutomaton (states, alphabet, q1, acceptStates, transitionFunction, Symbol.EPSILON, stackAlphabet);

            var negative = Word.Parse ("00001101", alphabet);
            var positive = Word.Parse ("00001111", alphabet);

            Assert.False (automaton.Accepts (negative));
            Assert.True (automaton.Accepts (positive));

            for (var i = 0; i < 50; i++)
            {
                var startWith1 = Random.Next (2) == 1;
                var randomNegative1 = string.Join ("",
                                                   Enumerable.Repeat (startWith1 ? 1 : 0, Random.Next (20)).Concat (
                                                       Enumerable.Repeat (startWith1 ? 0 : 1, Random.Next (20)).Concat (
                                                           Enumerable.Repeat (startWith1 ? 1 : 0, Random.Next (20)))));
                var randomNegative2 = string.Join ("",
                                                   Enumerable.Repeat (0, Random.Next (20)).Concat (
                                                       Enumerable.Repeat (1, Random.Next (20, 40))));

                var positiveCount  = Random.Next (20);
                var randomPositive = string.Join ("", Enumerable.Repeat (0, positiveCount).Concat (Enumerable.Repeat (1, positiveCount)));

                Assert.False (automaton.Accepts (Word.Parse (randomNegative1, alphabet)));
                Assert.False (automaton.Accepts (Word.Parse (randomNegative2, alphabet)));
                Assert.True (automaton.Accepts (Word.Parse (randomPositive, alphabet)), $"The automaton should have recognized '{randomPositive}'");
            }
        }
    }
}