using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "AttackDebuff",
        menuName = "NAPI/Status Effects/Debuff/Attack")]
    public class AttackDebuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.Attack * percentage);

            return new AttackDebuffEffect(duration, amount);
        }
    }
}
