using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "ElectricFibrillation", menuName = "NAPI/Status Effects/DOT/Fibrilación Eléctrica (Rayo)")]
    public class ElectricFibrillationData : DamageOverTimeEffectData
    {
        [Header("Extra: interrupción")]
        [Tooltip("Probabilidad, en cada turno del objetivo, de que su acción sea interrumpida por completo.")]
        [Range(0f, 1f)] public float interruptChance = 0.15f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            int damagePerTurn = DamageScalingUtility.Calculate(scalingType, scalingValue, source, target, damageResult);
            return new ElectricFibrillationEffect(duration, damagePerTurn, interruptChance, effectName);
        }
    }
}
