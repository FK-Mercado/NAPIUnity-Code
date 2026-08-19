using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "DefenseDebuff",
        menuName = "NAPI/Status Effects/Debuff/Defense")]
    public class DefenseDebuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.Defense * percentage);

            return new DefenseDebuffEffect(duration, amount);
        }
    }
}
