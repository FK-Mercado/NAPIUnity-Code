using UnityEngine;

namespace NAPI.Combat
{
    public class SpeedDebuffEffect : StatusEffect
    {
        private readonly int amount;

        public SpeedDebuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Debuff de Velocidad";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifySpeed(-amount);
            Debug.Log($"{target.Data.displayName} pierde {amount} de velocidad ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifySpeed(amount);
            Debug.Log($"{target.Data.displayName} recupera la velocidad perdida (+{amount}).");
        }
    }
}
