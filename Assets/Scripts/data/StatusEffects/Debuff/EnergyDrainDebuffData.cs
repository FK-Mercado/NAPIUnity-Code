using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "EnergyDrainDebuff",
        menuName = "NAPI/Status Effects/Debuff/Energy Drain")]
    public class EnergyDrainDebuffData : StatModifierEffectData
    {
        public override StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult)
        {
            int amountPerTurn = Mathf.RoundToInt(target.MaxEnergy * percentage);

            return new EnergyDrainEffect(duration, amountPerTurn);
        }
    }
}
