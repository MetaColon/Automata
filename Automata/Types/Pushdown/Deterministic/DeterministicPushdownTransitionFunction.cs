using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Pushdown.Deterministic
{
    public class DeterministicPushdownTransitionFunction : PushdownTransitionFunction
    {
        public DeterministicPushdownTransitionFunction (HashSet <DeterministicPushdownPartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet <DeterministicPushdownPartialTransitionFunction> PartialTransitionFunctions { get; }

        public override bool Equals (object obj)
            => obj is DeterministicPushdownTransitionFunction pushdownTransitionFunction && Equals (pushdownTransitionFunction);

        protected bool Equals (DeterministicPushdownTransitionFunction other)
            => Equals (PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode ()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode () : 0;

        public PushdownConfiguration GetNextConfiguration (PushdownConfiguration currentConfiguration)
        {
            var nextStackSymbol = currentConfiguration.Stack.Peek ();
            var nextInputSymbol = currentConfiguration.LeftSymbols.Peek ();
            var currentState    = currentConfiguration.State;

            // As it's deterministic, a maximum of one of the following return statements should be allowed by the if conditions surrounding them.
            PushdownConfiguration nextConfiguration = null;

            var epsilonEpsilonMove = GetNextState (currentState, Symbol.EPSILON, Symbol.EPSILON);
            if (epsilonEpsilonMove.HasValue)
                nextConfiguration = new PushdownConfiguration (epsilonEpsilonMove.Value.State, currentConfiguration.Stack.Push (epsilonEpsilonMove.Value.Symbol), currentConfiguration.LeftSymbols.Clone ());

            if (!nextStackSymbol.IsEpsilon)
            {
                var epsilonStackMove = GetNextState (currentState, Symbol.EPSILON, nextStackSymbol);
                if (epsilonStackMove.HasValue)
                {
                    if (nextConfiguration != null)
                        throw new Exception ("This automaton was not deterministic!");

                    nextConfiguration = new PushdownConfiguration (epsilonStackMove.Value.State, currentConfiguration.Stack.OverwriteTop (epsilonStackMove.Value.Symbol), currentConfiguration.LeftSymbols.Clone ());
                }
            }

            if (!nextInputSymbol.IsEpsilon)
            {
                var inputEpsilonMove = GetNextState (currentState, nextInputSymbol, Symbol.EPSILON);
                if (inputEpsilonMove.HasValue)
                {
                    if (nextConfiguration != null)
                        throw new Exception ("This automaton was not deterministic!");

                    nextConfiguration = new PushdownConfiguration (inputEpsilonMove.Value.State, currentConfiguration.Stack.Push (inputEpsilonMove.Value.Symbol), currentConfiguration.LeftSymbols.SkipNext ());
                }
            }

            if (!nextInputSymbol.IsEpsilon && !nextStackSymbol.IsEpsilon)
            {
                var inputStackMove = GetNextState (currentState, nextInputSymbol, nextStackSymbol);
                if (inputStackMove.HasValue)
                {
                    if (nextConfiguration != null)
                        throw new Exception ("This automaton was not deterministic!");

                    nextConfiguration = new PushdownConfiguration (inputStackMove.Value.State, currentConfiguration.Stack.OverwriteTop (inputStackMove.Value.Symbol), currentConfiguration.LeftSymbols.SkipNext ());
                }
            }

            return nextConfiguration;
        }

        private (State State, Symbol Symbol)? GetNextState (State currentState, Symbol inputSymbol, Symbol inputStackSymbol)
            => PartialTransitionFunctions.FirstOrDefault (function => function.InputState.Equals (currentState) && function.InputStackSymbol.Equals (inputStackSymbol) && function.InputSymbol.Equals (inputSymbol))?.GetResultState ();
    }
}