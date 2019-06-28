using System.Collections.Generic;
using System.Linq;

namespace DeterministicAutomata.Types.General
{
    public class Word
    {
        public Word(Queue<Symbol> inputSymbols)
            => InputSymbols = inputSymbols;

        public Queue<Symbol> InputSymbols { get; }

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

            var inputSymbols = new Queue<Symbol>(input.Length);

            foreach (var c in input.Reverse())
            {
                var symbol = alphabet.InputSymbols.FirstOrDefault(inputSymbol => inputSymbol.Value[0] == c);
                if (symbol == null)
                    return null;

                inputSymbols.Enqueue(symbol);
            }

            return new Word(inputSymbols);
        }

        public Symbol Peek()
            => InputSymbols.TryPeek(out var symbol) ? symbol : null;

        public Word SkipNext()
            => new Word(InputSymbols.Count > 0 ? new Queue<Symbol>(InputSymbols.Skip(1)) : new Queue<Symbol>());

        public int Count()
            => InputSymbols.Count;
    }
}