using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Types.Pushdown
{
    public class PushdownConfiguration : Configuration
    {
        public PushdownConfiguration (State state, Stack stack, Word leftSymbols) : base (state, leftSymbols)
            => Stack = stack;

        public Stack Stack { get; }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is PushdownConfiguration configuration && Equals (configuration);

        protected bool Equals (PushdownConfiguration other)
            => Equals (Stack, other.Stack) && base.Equals (other);

        /// <inheritdoc />
        public override int GetHashCode ()
            => Stack != null ? Stack.GetHashCode () : 0;

        /// <inheritdoc />
        public override string ToString ()
            => $"State: {State}; Stack: {Stack}; LeftSymbols: {LeftSymbols}";
    }
}