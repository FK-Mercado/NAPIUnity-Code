using UnityEngine;

namespace NAPI.Combat
{
    public class HealthRegenEffect : StatusEffect
    {
        private readonly int amountPerTurn;

        public HealthRegenEffect(int duration, int amountPerTurn)
            : base(duration)
        {
            EffectName = "Regeneración de Vida";
            this.amountPerTurn = amountPerTurn;
        }

        public override void OnTurnStart(Combatant target)
        {
            target.Heal(amountPerTurn);
            Debug.Log($"{target.Data.displayName} recupera {amountPerTurn} de vida.");
        }
    }
}
