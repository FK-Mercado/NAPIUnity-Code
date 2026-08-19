using UnityEngine;

namespace NAPI.Combat
{
    public class AttackDebuffEffect : StatusEffect
    {
        private readonly int amount;

        public AttackDebuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Debuff de Ataque";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifyAttack(-amount);
            Debug.Log($"{target.Data.displayName} pierde {amount} de ataque ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifyAttack(amount);
            Debug.Log($"{target.Data.displayName} recupera el ataque perdido (+{amount}).");
        }
    }
}
