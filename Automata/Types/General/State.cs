namespace Automata.Types.General
{
    public class State
    {
        public State(string name)
            => Name = name;

        public string Name { get; }

        public override bool Equals(object obj)
            => obj is State state && Equals(state);

        protected bool Equals(State other)
            => string.Equals(Name, other.Name);

        public override int GetHashCode()
            => Name != null ? Name.GetHashCode() : 0;
    }
}