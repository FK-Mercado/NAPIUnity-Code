using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "HealthDrainDebuff",
        menuName = "NAPI/Status Effects/Debuff/Health Drain")]
    public class HealthDrainDebuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amountPerTurn = Mathf.RoundToInt(target.MaxHP * percentage);

            return new HealthDrainEffect(duration, amountPerTurn);
        }
    }
}
