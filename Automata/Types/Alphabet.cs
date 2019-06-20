using System.Collections.Generic;

namespace Automata.Types
{
    public class Alphabet
    {
        public Alphabet(HashSet<InputSymbol> inputSymbols)
            => InputSymbols = inputSymbols;

        public HashSet<InputSymbol> InputSymbols { get; }

        public override bool Equals(object obj)
            => obj is Alphabet alphabet && Equals(alphabet);

        protected bool Equals(Alphabet other)
            => Equals(InputSymbols, other.InputSymbols);

        public override int GetHashCode()
            => InputSymbols != null ? InputSymbols.GetHashCode() : 0;
    }
}