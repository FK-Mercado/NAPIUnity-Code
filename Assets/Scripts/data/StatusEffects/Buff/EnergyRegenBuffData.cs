using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "EnergyRegenBuff",
        menuName = "NAPI/Status Effects/Buff/Energy Regen")]
    public class EnergyRegenBuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amountPerTurn = Mathf.RoundToInt(target.MaxEnergy * percentage);

            return new EnergyRegenEffect(duration, amountPerTurn);
        }
    }
}
