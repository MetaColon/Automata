namespace Automata.Types
{
    public class InputSymbol
    {
        public InputSymbol(string value)
            => this.Value = value;

        public string Value { get; }

        public override bool Equals(object obj)
            => obj is InputSymbol inputSymbol && Equals(inputSymbol);

        protected bool Equals(InputSymbol other)
            => string.Equals(this.Value, other.Value);

        public override int GetHashCode()
            => this.Value != null ? this.Value.GetHashCode() : 0;
    }
}