using System.Collections.Generic;
using System.Linq;

namespace Automata.Types
{
    public class Word
    {
        public Word(List<InputSymbol> inputSymbols)
            => this.InputSymbols = inputSymbols;

        public List<InputSymbol> InputSymbols { get; }

        public override bool Equals(object obj)
            => obj is Word word && Equals(word);

        protected bool Equals(Word other)
            => Equals(this.InputSymbols, other.InputSymbols);

        public override int GetHashCode()
            => this.InputSymbols != null ? this.InputSymbols.GetHashCode() : 0;

        public static Word Parse(string input, Alphabet alphabet)
        {
            if (alphabet.InputSymbols.Any(symbol => symbol.Value.Length != 1))
                return null;

            List<InputSymbol> inputSymbols = new List<InputSymbol>(input.Length);

            foreach (char c in input)
            {
                InputSymbol symbol = alphabet.InputSymbols.FirstOrDefault(inputSymbol => inputSymbol.Value[0] == c);
                if (symbol == null)
                    return null;

                inputSymbols.Add(symbol);
            }

            return new Word(inputSymbols);
        }
    }
}