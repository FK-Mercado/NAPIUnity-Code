using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "PurgeBuffs", menuName = "NAPI/Status Effects/Debuff/Clean Buffs")]
    public class PurgeBuffsDebuffData : StatusEffectData
    {
        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new PurgeBuffsEffect(duration);
        }
    }
}
