using System.Collections.Generic;

namespace Automata.Types
{
    public class Alphabet
    {
        public Alphabet(HashSet<InputSymbol> inputSymbols)
            => this.InputSymbols = inputSymbols;

        public HashSet<InputSymbol> InputSymbols { get; }

        public override bool Equals(object obj)
            => obj is Alphabet alphabet && Equals(alphabet);

        protected bool Equals(Alphabet other)
            => Equals(this.InputSymbols, other.InputSymbols);

        public override int GetHashCode()
            => this.InputSymbols != null ? this.InputSymbols.GetHashCode() : 0;
    }
}