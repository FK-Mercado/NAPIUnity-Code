using UnityEngine;

namespace NAPI.Combat
{
    public class DefenseBuffEffect : StatusEffect
    {
        private readonly int amount;

        public DefenseBuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Buff de Defensa";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifyDefense(amount);
            Debug.Log($"{target.Data.displayName} gana +{amount} de defensa ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifyDefense(-amount);
            Debug.Log($"{target.Data.displayName} pierde el buff de defensa (-{amount}).");
        }
    }
}
