using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "MaxHPDebuff",
        menuName = "NAPI/Status Effects/Debuff/Max HP")]
    public class MaxHPDebuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.MaxHP * percentage);

            return new MaxHPDebuffEffect(duration, amount);
        }
    }
}
