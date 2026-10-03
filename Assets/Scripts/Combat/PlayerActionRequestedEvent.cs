using NAPI.Combat;

namespace NAPI.Combat
{
    public class PlayerActionRequestedEvent
    {
        public Combatant Combatant { get; }

        public PlayerActionRequestedEvent(Combatant combatant)
        {
            Combatant = combatant;
        }
    }
}