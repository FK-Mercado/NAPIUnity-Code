using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "MaxHPBuff",
        menuName = "NAPI/Status Effects/Buff/Max HP")]
    public class MaxHPBuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.MaxHP * percentage);

            return new MaxHPBuffEffect(duration, amount);
        }
    }
}
