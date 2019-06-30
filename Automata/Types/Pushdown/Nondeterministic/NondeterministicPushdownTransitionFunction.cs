using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;
using Automata.Types.Pushdown.Deterministic;


namespace Automata.Types.Pushdown.Nondeterministic
{
    public class NondeterministicPushdownTransitionFunction : PushdownTransitionFunction
    {
        public NondeterministicPushdownTransitionFunction (HashSet <NondeterministicPushdownPartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet <NondeterministicPushdownPartialTransitionFunction> PartialTransitionFunctions { get; }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is NondeterministicPushdownTransitionFunction function && Equals (function);

        protected bool Equals (NondeterministicPushdownTransitionFunction other)
            => Equals (PartialTransitionFunctions, other.PartialTransitionFunctions);

        /// <inheritdoc />
        public override int GetHashCode ()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode () : 0;

        public HashSet <PushdownConfiguration> GetNextConfigurations (IEnumerable <PushdownConfiguration> currentConfigurations)
            => currentConfigurations.SelectMany (GetNextConfigurations).ToHashSet ();

        public HashSet <PushdownConfiguration> GetNextConfigurations (PushdownConfiguration currentConfiguration)
        {
            var nextStackSymbol = currentConfiguration.Stack.Peek ();
            var nextInputSymbol = currentConfiguration.LeftSymbols.Peek ();
            var currentState    = currentConfiguration.State;

            var nextConfigurations = new HashSet <PushdownConfiguration> ();

            var epsilonEpsilonMove = GetNextStates (currentState, Symbol.EPSILON, Symbol.EPSILON);
            foreach (var (state, symbol) in epsilonEpsilonMove)
                nextConfigurations.Add (new PushdownConfiguration (state, currentConfiguration.Stack.Push (symbol), currentConfiguration.LeftSymbols.Clone ()));

            if (!nextStackSymbol.IsEpsilon)
            {
                var epsilonStackMove = GetNextStates (currentState, Symbol.EPSILON, nextStackSymbol);
                foreach (var (state, symbol) in epsilonStackMove)
                    nextConfigurations.Add (new PushdownConfiguration (state, currentConfiguration.Stack.OverwriteTop (symbol), currentConfiguration.LeftSymbols.Clone ()));
            }

            if (!nextInputSymbol.IsEpsilon)
            {
                var inputEpsilonMove = GetNextStates (currentState, nextInputSymbol, Symbol.EPSILON);
                foreach (var (state, symbol) in inputEpsilonMove)
                    nextConfigurations.Add (new PushdownConfiguration (state, currentConfiguration.Stack.Push (symbol), currentConfiguration.LeftSymbols.SkipNext ()));
            }

            if (!nextInputSymbol.IsEpsilon && !nextStackSymbol.IsEpsilon)
            {
                var inputStackMove = GetNextStates (currentState, nextInputSymbol, nextStackSymbol);
                foreach (var (state, symbol) in inputStackMove)
                    nextConfigurations.Add (new PushdownConfiguration (state, currentConfiguration.Stack.OverwriteTop (symbol), currentConfiguration.LeftSymbols.SkipNext ()));
            }

            return nextConfigurations;
        }

        private HashSet <(State State, Symbol Symbol)> GetNextStates (State currentState, Symbol inputSymbol, Symbol inputStackSymbol)
            => PartialTransitionFunctions.Where (function
                                                     => function.InputState.Equals (currentState) &&
                                                        function.InputStackSymbol.Equals (inputStackSymbol) &&
                                                        function.InputSymbol.Equals (inputSymbol)).SelectMany (function => function.GetResultStates ()).ToHashSet ();
    }
}