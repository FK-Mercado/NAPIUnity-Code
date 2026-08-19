using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class EnergyCombustionEffect : StatusEffect
    {
        private readonly float multiplierIncrease;
        private readonly float recoilPercentage;

        public EnergyCombustionEffect(int duration, float multiplierIncrease, float recoilPercentage) : base(duration)
        {
            EffectName = "Combustión de Energía";
            this.multiplierIncrease = multiplierIncrease;
            this.recoilPercentage = recoilPercentage;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifyEnergyCostMultiplier(multiplierIncrease);
            Debug.Log($"{target.Data.displayName} sufre Combustión de Energía: sus habilidades cuestan más EN.");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifyEnergyCostMultiplier(-multiplierIncrease);
        }

        // El propio afectado recibe una fracción del daño que él mismo inflige.
        public override void OnDamageDealtByHolder(Combatant holder, Combatant target, SkillData skill, int damageDealt, bool wasCrit)
        {
            int recoil = Mathf.RoundToInt(damageDealt * recoilPercentage);
            if (recoil <= 0) return;

            holder.TakeDamage(recoil);
            Debug.Log($"{holder.Data.displayName} recibe {recoil} de daño por Combustión de Energía.");
        }
    }
}
