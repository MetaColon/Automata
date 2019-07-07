using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Turing.Basic
{
    public class BasicTuringTransitionFunction
    {
        public BasicTuringTransitionFunction (HashSet <BasicTuringPartialTransitionFunction> partialTransitionFunctions)
            => PartialTransitionFunctions = partialTransitionFunctions;

        public HashSet <BasicTuringPartialTransitionFunction> PartialTransitionFunctions { get; }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is BasicTuringTransitionFunction function && Equals (function);

        protected bool Equals (BasicTuringTransitionFunction other)
            => Equals (PartialTransitionFunctions, other.PartialTransitionFunctions);

        /// <inheritdoc />
        public override int GetHashCode ()
            => PartialTransitionFunctions != null ? PartialTransitionFunctions.GetHashCode () : 0;

        public BasicTuringConfiguration GetNextConfiguration (BasicTuringConfiguration currentConfiguration)
        {
            var partialFunction = PartialTransitionFunctions.FirstOrDefault (function => function.InputState.Equals (currentConfiguration.State) && function.InputSymbol.Equals (currentConfiguration.GetCurrentSymbol ()));

            // Halt
            if (partialFunction == null)
                return currentConfiguration.Halt ();

            var newTape      = currentConfiguration.Tape.Write (currentConfiguration.HeadIndex, partialFunction.ResultSymbol);
            var newHeadIndex = partialFunction.GetNewHeadIndex (currentConfiguration.HeadIndex);
            var newState     = partialFunction.ResultState;

            return new BasicTuringConfiguration (newHeadIndex, newTape, newState);
        }
    }
}