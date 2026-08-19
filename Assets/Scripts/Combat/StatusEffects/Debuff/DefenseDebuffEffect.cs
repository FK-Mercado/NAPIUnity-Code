using UnityEngine;

namespace NAPI.Combat
{
    public class DefenseDebuffEffect : StatusEffect
    {
        private readonly int amount;

        public DefenseDebuffEffect(int duration, int amount)
            : base(duration)
        {
            EffectName = "Debuff de Defensa";
            this.amount = amount;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifyDefense(-amount);
            Debug.Log($"{target.Data.displayName} pierde {amount} de defensa ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifyDefense(amount);
            Debug.Log($"{target.Data.displayName} recupera la defensa perdida (+{amount}).");
        }
    }
}
