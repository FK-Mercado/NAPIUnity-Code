using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "SoulCorruptionDebuff", menuName = "NAPI/Status Effects/Debuff/Soul Corruption")]
    public class SoulCorruptionDebuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            int amountPerTurn = Mathf.RoundToInt(target.MaxHP * percentage);
            return new SoulCorruptionEffect(duration, amountPerTurn);
        }
    }
}
