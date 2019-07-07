using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Types.Turing
{
    public class TapeAlphabet
    {
        public TapeAlphabet (HashSet <TapeSymbol> tapeSymbols)
        {
            TapeSymbols = tapeSymbols;
            if (!TapeSymbols.Contains (TapeSymbol.BLANK))
                TapeSymbols.Add (TapeSymbol.BLANK);
        }

        public HashSet <TapeSymbol> TapeSymbols { get; }

        /// <inheritdoc />
        public override bool Equals (object obj)
            => obj is TapeAlphabet alphabet && Equals (alphabet);

        protected bool Equals (TapeAlphabet other)
            => Equals (TapeSymbols, other.TapeSymbols);

        /// <inheritdoc />
        public override int GetHashCode () => TapeSymbols != null ? TapeSymbols.GetHashCode () : 0;
    }
}