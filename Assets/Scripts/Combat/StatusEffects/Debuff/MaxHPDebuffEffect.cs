using UnityEngine;

namespace NAPI.Combat
{
    public class MaxHPDebuffEffect : StatusEffect
    {
        private readonly int amount;

        public MaxHPDebuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Debuff de Vida Máxima";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.DecreaseMaxHP(amount);
            Debug.Log($"{target.Data.displayName} pierde {amount} de vida máxima ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.IncreaseMaxHP(amount);
            Debug.Log($"{target.Data.displayName} recupera la vida máxima perdida (+{amount}).");
        }
    }
}
