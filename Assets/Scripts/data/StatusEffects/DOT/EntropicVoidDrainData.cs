using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "EntropicVoidDrain", menuName = "NAPI/Status Effects/DOT/Vaciamiento Entrópico (Oscuridad)")]
    public class EntropicVoidDrainData : DamageOverTimeEffectData
    {
        [Header("Extra: reducción de Energía Máxima")]
        [Range(0f, 1f)] public float maxEnergyReductionPercentage = 0.1f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            int damagePerTurn = DamageScalingUtility.Calculate(scalingType, scalingValue, source, target, damageResult);
            int maxEnergyReduction = Mathf.RoundToInt(target.MaxEnergy * maxEnergyReductionPercentage);

            return new EntropicVoidDrainEffect(duration, damagePerTurn, maxEnergyReduction, effectName);
        }
    }
}
