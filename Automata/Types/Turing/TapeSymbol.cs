using Automata.Types.General;


namespace Automata.Types.Turing
{
    public class TapeSymbol : Symbol
    {
        /// <inheritdoc />
        public TapeSymbol (string value, bool isBlank) : base (value)
            => IsBlank = isBlank;

        public bool IsBlank { get; }

        public static readonly TapeSymbol BLANK = new TapeSymbol (" ", true);

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is TapeSymbol symbol && Equals (symbol);

        protected bool Equals (TapeSymbol other)
            => base.Equals (other) || IsBlank == other.IsBlank;

        /// <inheritdoc />
        public override int GetHashCode ()
        {
            unchecked
            {
                return (base.GetHashCode () * 397) ^ IsBlank.GetHashCode ();
            }
        }
    }
}