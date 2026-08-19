using UnityEngine;

namespace NAPI.Combat
{
    public class EntropicVoidDrainEffect : StatusEffect
    {
        private readonly int damagePerTurn;
        private readonly int maxEnergyReduction;

        public EntropicVoidDrainEffect(int duration, int damagePerTurn, int maxEnergyReduction, string effectName)
            : base(duration)
        {
            EffectName = effectName;
            this.damagePerTurn = damagePerTurn;
            this.maxEnergyReduction = maxEnergyReduction;
        }

        public override void OnApply(Combatant target)
        {
            target.DecreaseMaxEnergy(maxEnergyReduction);
            Debug.Log($"{target.Data.displayName} pierde {maxEnergyReduction} de energía máxima por {EffectName}.");
        }

        public override void OnTurnStart(Combatant target)
        {
            target.TakeDamage(damagePerTurn);
            Debug.Log($"{target.Data.displayName} recibe {damagePerTurn} de {EffectName}.");
        }

        public override void OnRemove(Combatant target)
        {
            target.IncreaseMaxEnergy(maxEnergyReduction);
        }
    }
}
