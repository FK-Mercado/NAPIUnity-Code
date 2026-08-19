using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "SpeedDebuff",
        menuName = "NAPI/Status Effects/Debuff/Speed")]
    public class SpeedDebuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amount = Mathf.RoundToInt(target.Speed * percentage);

            return new SpeedDebuffEffect(duration, amount);
        }
    }
}
