using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "HydrostaticPressure", menuName = "NAPI/Status Effects/DOT/Presión Hidrostática (Agua)")]
    public class HydrostaticPressureData : DamageOverTimeEffectData
    {
        [Header("Extra: reducción de regeneración de energía")]
        [Range(0f, 1f)] public float regenReduction = 0.5f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            int damagePerTurn = DamageScalingUtility.Calculate(scalingType, scalingValue, source, target, damageResult);
            return new HydrostaticPressureEffect(duration, damagePerTurn, regenReduction, effectName);
        }
    }
}
