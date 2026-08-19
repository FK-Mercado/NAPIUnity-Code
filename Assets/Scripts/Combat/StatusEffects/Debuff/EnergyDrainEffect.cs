using UnityEngine;

namespace NAPI.Combat
{
    public class EnergyDrainEffect : StatusEffect
    {
        private readonly int amountPerTurn;

        public EnergyDrainEffect(int duration, int amountPerTurn)
            : base(duration)
        {
            EffectName = "Drenaje de Energía";
            this.amountPerTurn = amountPerTurn;
        }

        public override void OnTurnStart(Combatant target)
        {
            target.SpendEnergy(amountPerTurn);
            Debug.Log($"{target.Data.displayName} pierde {amountPerTurn} de energía.");
        }
    }
}
