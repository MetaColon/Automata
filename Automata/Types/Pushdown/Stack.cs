using System.Collections.Generic;

using Automata.Types.General;


namespace Automata.Types.Pushdown
{
    public class Stack
    {
        public Stack(Stack<Symbol> content)
            => Content = content;

        public Stack<Symbol> Content { get; }

        public override bool Equals(object obj)
            => obj is Stack stack && Equals(stack);

        protected bool Equals(Stack other)
            => Equals(Content, other.Content);

        public override int GetHashCode()
            => Content != null ? Content.GetHashCode() : 0;

        public Symbol Pop()
            => Content.TryPop(out var s) ? s : Symbol.EPSILON;

        public void Push(Symbol symbol)
            => Content.Push(symbol);
    }
}