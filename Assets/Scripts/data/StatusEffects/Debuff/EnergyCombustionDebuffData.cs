using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "EnergyCombustionDebuff", menuName = "NAPI/Status Effects/Debuff/Energy Combustion")]
    public class EnergyCombustionDebuffData : StatusEffectData
    {
        [Header("Combustión de Energía")]
        [Tooltip("1 = el costo de energía se duplica (multiplicador base + este valor)")]
        [Range(0f, 3f)] public float energyCostMultiplierIncrease = 1f;
        [Range(0f, 1f)] public float recoilPercentage = 0.1f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new EnergyCombustionEffect(duration, energyCostMultiplierIncrease, recoilPercentage);
        }
    }
}
