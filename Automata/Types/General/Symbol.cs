namespace Automata.Types.General
{
    public class Symbol
    {
        public Symbol(string value, bool isEpsilon = false)
        {
            Value = value;
            IsEpsilon = isEpsilon;
        }

        public string Value { get; }
        public bool IsEpsilon { get; }

        public override bool Equals(object obj)
            => obj is Symbol inputSymbol && Equals(inputSymbol);

        protected bool Equals(Symbol other)
            => string.Equals(Value, other.Value) && IsEpsilon == other.IsEpsilon;

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Value != null ? Value.GetHashCode() : 0) * 397) ^ IsEpsilon.GetHashCode();
            }
        }

        public static Symbol EPSILON = new Symbol("ε", true);

        /// <inheritdoc />
        public override string ToString () => Value;
    }
}