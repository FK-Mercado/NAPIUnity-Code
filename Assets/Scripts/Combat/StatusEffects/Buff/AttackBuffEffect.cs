using UnityEngine;

namespace NAPI.Combat
{
    public class AttackBuffEffect : StatusEffect
    {
        private readonly int amount;

        public AttackBuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Buff de Ataque";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifyAttack(amount);
            Debug.Log($"{target.Data.displayName} gana +{amount} de ataque ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifyAttack(-amount);
            Debug.Log($"{target.Data.displayName} pierde el buff de ataque (-{amount}).");
        }
    }
}
