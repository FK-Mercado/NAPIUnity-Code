using UnityEngine;

namespace NAPI.Combat
{
    public class HydrostaticPressureEffect : StatusEffect
    {
        private readonly int damagePerTurn;
        private readonly float regenReduction;

        public HydrostaticPressureEffect(int duration, int damagePerTurn, float regenReduction, string effectName)
            : base(duration)
        {
            EffectName = effectName;
            this.damagePerTurn = damagePerTurn;
            this.regenReduction = regenReduction;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifyEnergyRegenMultiplier(-regenReduction);
            Debug.Log($"{target.Data.displayName} recupera energía más lento por {EffectName}.");
        }

        public override void OnTurnStart(Combatant target)
        {
            target.TakeDamage(damagePerTurn);
            Debug.Log($"{target.Data.displayName} recibe {damagePerTurn} de {EffectName}.");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifyEnergyRegenMultiplier(regenReduction);
        }
    }
}
