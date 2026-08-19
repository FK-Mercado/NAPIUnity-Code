using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "CleanseBuff", menuName = "NAPI/Status Effects/Buff/Clean Debuff")]
    public class CleanseBuffData : StatusEffectData
    {
        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new CleanseEffect(duration);
        }
    }
}
