using System.Collections.Generic;
using System.Linq;

using Automata.Types.General;


namespace Automata.Types.Pushdown
{
    public class Stack
    {
        public static Stack Empty ()
            => new Stack (new Stack <Symbol> ());

        public Stack (Stack <Symbol> content)
            => Content = content;

        public Stack (Symbol initialSymbol)
        {
            Content = new Stack <Symbol> ();
            Push (initialSymbol);
        }

        public Stack <Symbol> Content { get; }

        public override bool Equals (object obj)
            => obj is Stack stack && Equals (stack);

        protected bool Equals (Stack other)
            => Content == null && other == null || Content != null && other?.Content != null && Content.SequenceEqual (other.Content);

        public override int GetHashCode ()
            => Content != null ? Content.GetHashCode () : 0;

        public Stack Push (Symbol symbol)
        {
            var newContent = new Stack <Symbol> (new Stack <Symbol> (Content));

            if (symbol.IsEpsilon)
                return new Stack (newContent);

            newContent.Push (symbol);
            return new Stack (newContent);
        }

        public Symbol Peek ()
            => Content.TryPeek (out var s) ? s : Symbol.EPSILON;

        public Stack SkipNext ()
            => new Stack (new Stack <Symbol> (new Stack <Symbol> (Content.Count > 0 ? Content.Skip (1) : Content)));

        public Stack OverwriteTop (Symbol symbol)
        {
            var skipped = SkipNext ();
            skipped.Push (symbol);

            return skipped;
        }

        /// <inheritdoc />
        public override string ToString ()
            => string.Join (", ", Content);
    }
}