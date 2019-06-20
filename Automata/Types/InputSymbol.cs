namespace Automata.Types
{
    public class InputSymbol
    {
        public InputSymbol(string value)
            => Value = value;

        public string Value { get; }

        public override bool Equals(object obj)
            => obj is InputSymbol inputSymbol && Equals(inputSymbol);

        protected bool Equals(InputSymbol other)
            => string.Equals(Value, other.Value);

        public override int GetHashCode()
            => Value != null ? Value.GetHashCode() : 0;
    }
}