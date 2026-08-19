using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "ParalysisDebuff", menuName = "NAPI/Status Effects/Debuff/Parálisis")]
    public class ParalysisDebuffData : StatusEffectData
    {
        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new ParalysisEffect(duration);
        }
    }
}
