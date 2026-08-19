using UnityEngine;

namespace NAPI.Combat
{
    public class SpeedBuffEffect : StatusEffect
    {
        private readonly int amount;

        public SpeedBuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Buff de Velocidad";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifySpeed(amount);
            Debug.Log($"{target.Data.displayName} gana +{amount} de velocidad ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifySpeed(-amount);
            Debug.Log($"{target.Data.displayName} pierde el buff de velocidad (-{amount}).");
        }
    }
}
