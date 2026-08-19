using UnityEngine;

namespace NAPI.Combat
{
    public class MaxHPBuffEffect : StatusEffect
    {
        private readonly int amount;

        public MaxHPBuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Buff de Vida Máxima";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.IncreaseMaxHP(amount);
            Debug.Log($"{target.Data.displayName} gana +{amount} de vida máxima ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.DecreaseMaxHP(amount);
            Debug.Log($"{target.Data.displayName} pierde el buff de vida máxima (-{amount}).");
        }
    }
}
