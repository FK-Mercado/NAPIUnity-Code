using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "AttackBuff",
        menuName = "NAPI/Status Effects/Buff/Attack")]
    public class AttackBuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.Attack * percentage);

            return new AttackBuffEffect(duration, amount);
        }
    }
}
