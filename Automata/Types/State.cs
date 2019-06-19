namespace Automata.Types
{
    public class State
    {
        public State(string name)
            => this.Name = name;

        public string Name { get; }

        public override bool Equals(object obj)
            => obj is State state && Equals(state);

        protected bool Equals(State other)
            => string.Equals(this.Name, other.Name);

        public override int GetHashCode()
            => this.Name != null ? this.Name.GetHashCode() : 0;
    }
}