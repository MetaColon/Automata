using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Finite.Nondeterministic
{
    public class NondeterministicFiniteTransitionFunction : FiniteTransitionFunction
    {
        public NondeterministicFiniteTransitionFunction (HashSet <NondeterministicFinitePartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet <NondeterministicFinitePartialTransitionFunction> PartialTransitionFunctions { get; }

        public override bool Equals (object obj)
            => obj is NondeterministicFiniteTransitionFunction transitionFunction && Equals (transitionFunction);

        protected bool Equals (NondeterministicFiniteTransitionFunction other)
            => Equals (PartialTransitionFunctions, other.PartialTransitionFunctions);

        public override int GetHashCode ()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode () : 0;

        public HashSet <FiniteConfiguration> GetNextConfigurations (FiniteConfiguration currentConfiguration)
            => GetNextStates (currentConfiguration.State, currentConfiguration.LeftSymbols.Peek ()).Select (state => new FiniteConfiguration (state, currentConfiguration.LeftSymbols.SkipNext ())).Union (
                GetNextStates (currentConfiguration.State, Symbol.EPSILON).Select (state => new FiniteConfiguration (state, currentConfiguration.LeftSymbols))).ToHashSet ();

        private HashSet <State> GetNextStates (State currentState, Symbol inputSymbol)
            => PartialTransitionFunctions.FirstOrDefault (function => function.InputState.Equals (currentState) && function.InputSymbol.Equals (inputSymbol))?.GetResultStates () ??
               new HashSet <State> ();
    }
}