using UnityEngine;

namespace NAPI.Combat
{
    public class EnergyRegenEffect : StatusEffect
    {
        private readonly int amountPerTurn;

        public EnergyRegenEffect(int duration, int amountPerTurn)
            : base(duration)
        {
            EffectName = "Regeneración de Energía";
            this.amountPerTurn = amountPerTurn;
        }

        public override void OnTurnStart(Combatant target)
        {
            // Escala por EnergyRegenMultiplier: si el objetivo tiene
            // Presión Hidrostática activa, recupera menos de lo normal.
            int scaled = Mathf.RoundToInt(amountPerTurn * target.EnergyRegenMultiplier);
            target.GainEnergy(scaled);
            Debug.Log($"{target.Data.displayName} recupera {scaled} de energía.");
        }
    }
}
