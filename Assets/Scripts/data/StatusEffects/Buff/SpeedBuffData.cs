using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "SpeedBuff",
        menuName = "NAPI/Status Effects/Buff/Speed")]
    public class SpeedBuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.Speed * percentage);

            return new SpeedBuffEffect(duration, amount);
        }
    }
}
