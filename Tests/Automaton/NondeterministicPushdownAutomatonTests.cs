using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Automaton.Pushdown;
using Automata.Types.General;
using Automata.Types.Pushdown.Nondeterministic;

using NUnit.Framework;


namespace Tests.Automaton
{
    public class NondeterministicPushdownAutomatonTests
    {
        private Random Random = new Random ();

        [Test]
        public void TestAIsBOrC ()
        {
            var q1           = new State ("q1");
            var q2           = new State ("q2");
            var q3           = new State ("q3");
            var q4           = new State ("q4");
            var q5           = new State ("q5");
            var q6           = new State ("q6");
            var q7           = new State ("q7");
            var states       = new HashSet <State> {q1, q2, q3, q4, q5, q6, q7};
            var acceptStates = new HashSet <State> {q4, q7};

            var ia       = new Symbol ("a");
            var ib       = new Symbol ("b");
            var ic       = new Symbol ("c");
            var symbols  = new HashSet <Symbol> {ia, ib, ic};
            var alphabet = new Alphabet (symbols);

            var sa            = new Symbol ("a");
            var se            = new Symbol ("$");
            var stackSymbols  = new HashSet <Symbol> {sa, se};
            var stackAlphabet = new Alphabet (stackSymbols);

            var transitionFunction = new NondeterministicPushdownTransitionFunction (new HashSet <NondeterministicPushdownPartialTransitionFunction>
            {
                new NondeterministicPushdownPartialTransitionFunction (q1, Symbol.EPSILON, Symbol.EPSILON, new HashSet <(State, Symbol)> {(q2, se)}),
                new NondeterministicPushdownPartialTransitionFunction (q2, ia, Symbol.EPSILON, new HashSet <(State, Symbol)> {(q2, sa)}),
                new NondeterministicPushdownPartialTransitionFunction (q2, Symbol.EPSILON, Symbol.EPSILON, new HashSet <(State, Symbol)> {(q3, Symbol.EPSILON), (q5, Symbol.EPSILON)}),
                new NondeterministicPushdownPartialTransitionFunction (q3, ib, sa, new HashSet <(State, Symbol)> {(q3, Symbol.EPSILON)}),
                new NondeterministicPushdownPartialTransitionFunction (q3, Symbol.EPSILON, se, new HashSet <(State, Symbol)> {(q4, Symbol.EPSILON)}),
                new NondeterministicPushdownPartialTransitionFunction (q4, ic, Symbol.EPSILON, new HashSet <(State, Symbol)> {(q4, Symbol.EPSILON)}),
                new NondeterministicPushdownPartialTransitionFunction (q5, ib, Symbol.EPSILON, new HashSet <(State, Symbol)> {(q5, Symbol.EPSILON)}),
                new NondeterministicPushdownPartialTransitionFunction (q5, Symbol.EPSILON, Symbol.EPSILON, new HashSet <(State, Symbol)> {(q6, Symbol.EPSILON)}),
                new NondeterministicPushdownPartialTransitionFunction (q6, ic, sa, new HashSet <(State, Symbol)> {(q6, Symbol.EPSILON)}),
                new NondeterministicPushdownPartialTransitionFunction (q6, Symbol.EPSILON, se, new HashSet <(State, Symbol)> {(q7, Symbol.EPSILON)})
            });

            // Accepts {a^ib^jc^k | i,j,k >= 0 and i = j or i = k
            var automaton = new NondeterministicPushdownAutomaton (
                states,
                alphabet,
                q1,
                acceptStates,
                Symbol.EPSILON,
                stackAlphabet,
                transitionFunction);

            var negative1 = Word.Parse ("abbccc", alphabet);
            var negative2 = Word.Parse ("ccbbaa", alphabet);
            var positive0 = Word.Parse ("", alphabet);
            var positive1 = Word.Parse ("aaabbbcc", alphabet);
            var positive2 = Word.Parse ("aaabbccc", alphabet);

            Assert.False (automaton.Accepts (negative1));
            Assert.False (automaton.Accepts (negative2));
            Assert.True (automaton.Accepts (positive0));
            Assert.True (automaton.Accepts (positive1));
            Assert.True (automaton.Accepts (positive2));

            for (var i = 0; i < 50; i++)
            {
                var countA = Random.Next (30);
                int countB, countC;
                do
                    countB = Random.Next (30);
                while (countB == countA);
                do
                    countC = Random.Next (30);
                while (countC == countA);

                var randomNegative = Word.Parse (string.Join ("",
                                                              Enumerable.Repeat ("a", countA).Concat (
                                                                  Enumerable.Repeat ("b", countB).Concat (
                                                                      Enumerable.Repeat ("c", countC)))), alphabet);

                var randomPositive1 = Word.Parse (string.Join ("",
                                                               Enumerable.Repeat ("a", countA).Concat (
                                                                   Enumerable.Repeat ("b", countA).Concat (
                                                                       Enumerable.Repeat ("c", countC)))), alphabet);

                var randomPositive2 = Word.Parse (string.Join ("",
                                                               Enumerable.Repeat ("a", countA).Concat (
                                                                   Enumerable.Repeat ("b", countB).Concat (
                                                                       Enumerable.Repeat ("c", countA)))), alphabet);

                Assert.False (automaton.Accepts (randomNegative), $"The automaton was not expected to accept {randomNegative}");
                Assert.True (automaton.Accepts (randomPositive1), $"The automaton was expected to accept {randomPositive1}");
                Assert.True (automaton.Accepts (randomPositive2), $"The automaton was expected to accept {randomPositive2}");
            }
        }
    }
}