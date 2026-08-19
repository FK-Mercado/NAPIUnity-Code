using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    /// <summary>
    /// "Ignorar parte de la defensa" se implementa como amplificar el
    /// daño ya calculado (que incluye la defensa) en vez de recalcularlo
    /// sin defensa — el resultado final es equivalente y no requiere
    /// exponer el desglose interno de DamageCalculator.
    /// </summary>
    public class SoftenDefenseEffect : StatusEffect
    {
        private readonly float amplification;

        public SoftenDefenseEffect(int duration, float amplification) : base(duration)
        {
            EffectName = "Soften Defense";
            this.amplification = amplification;
        }

        public override int ModifyIncomingDamage(Combatant holder, Combatant attacker, SkillData incomingSkill, int incomingDamage)
        {
            bool isMelee = incomingSkill.attackRange == AttackRangeType.Melee;
            bool hitsWeakness = incomingSkill.element == holder.Data.weakness;

            if (!isMelee && !hitsWeakness) return incomingDamage;

            int amplified = Mathf.RoundToInt(incomingDamage * (1f + amplification));
            Debug.Log($"{holder.Data.displayName} tiene la armadura ablandada: recibe daño amplificado.");
            return amplified;
        }
    }
}
