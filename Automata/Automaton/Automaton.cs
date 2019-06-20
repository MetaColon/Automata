using DeterministicAutomata.Types;
using DeterministicAutomata.Types.General;

namespace DeterministicAutomata.Automaton
{
    public interface Automaton
    {
        bool Accepts(Word word);
    }
}