using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "PurgeAll", menuName = "NAPI/Status Effects/Buff/Clean All")]
    public class PurgeAllBuffData : StatusEffectData
    {
        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new PurgeAllEffect(duration);
        }
    }
}
