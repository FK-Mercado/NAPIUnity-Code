using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "HealthRegenBuff",
        menuName = "NAPI/Status Effects/Buff/Health Regen")]
    public class HealthRegenBuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amountPerTurn = Mathf.RoundToInt(target.MaxHP * percentage);

            return new HealthRegenEffect(duration, amountPerTurn);
        }
    }
}
