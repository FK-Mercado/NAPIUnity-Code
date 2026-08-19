using UnityEngine;

namespace NAPI.Combat
{
    public class HealthDrainEffect : StatusEffect
    {
        private readonly int amountPerTurn;

        public HealthDrainEffect(int duration, int amountPerTurn)
            : base(duration)
        {
            EffectName = "Drenaje de Vida";
            this.amountPerTurn = amountPerTurn;
        }

        public override void OnTurnStart(Combatant target)
        {
            target.TakeDamage(amountPerTurn);
            Debug.Log($"{target.Data.displayName} pierde {amountPerTurn} de vida por debilitamiento.");
        }
    }
}
