using Automata.Types.General;


namespace Automata.Automaton
{
    public interface Automaton
    {
        bool Accepts(Word word);
    }
}