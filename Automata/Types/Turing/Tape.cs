using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Turing
{
    public class Tape
    {
        public Tape (List <TapeSymbol> symbols)
            => Symbols = symbols;

        public Tape (Word word) : this (word.InputSymbols.Select (symbol => new TapeSymbol (symbol.Value, false)).ToList ()) {}

        public List <TapeSymbol> Symbols { get; }

        public TapeSymbol Read (int position)
            => position < 0 || position >= Symbols.Count ? TapeSymbol.BLANK : Symbols [position];

        public Tape Write (int position, TapeSymbol symbol)
        {
            var newSymbols = new List <TapeSymbol> (Symbols);

            if (position < 0)
                return new Tape (newSymbols);

            while (position > newSymbols.Count)
                newSymbols.Add (TapeSymbol.BLANK);

            newSymbols [position] = symbol;

            return new Tape (newSymbols);
        }
    }
}