using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "DefenseBuff",
        menuName = "NAPI/Status Effects/Buff/Defense")]
    public class DefenseBuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.Defense * percentage);

            return new DefenseBuffEffect(duration, amount);
        }
    }
}
