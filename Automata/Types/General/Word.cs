using System.Collections.Generic;
using System.Linq;

namespace Automata.Types.General
{
    public class Word
    {
        public Word(List<InputSymbol> inputSymbols)
            => InputSymbols = inputSymbols;

        public List<InputSymbol> InputSymbols { get; }

        public override bool Equals(object obj)
            => obj is Word word && Equals(word);

        protected bool Equals(Word other)
            => Equals(InputSymbols, other.InputSymbols);

        public override int GetHashCode()
            => InputSymbols != null ? InputSymbols.GetHashCode() : 0;

        public static Word Parse(string input, Alphabet alphabet)
        {
            if (alphabet.InputSymbols.Any(symbol => symbol.Value.Length != 1))
                return null;

            var inputSymbols = new List<InputSymbol>(input.Length);

            foreach (var c in input)
            {
                var symbol = alphabet.InputSymbols.FirstOrDefault(inputSymbol => inputSymbol.Value[0] == c);
                if (symbol == null)
                    return null;

                inputSymbols.Add(symbol);
            }

            return new Word(inputSymbols);
        }
    }
}