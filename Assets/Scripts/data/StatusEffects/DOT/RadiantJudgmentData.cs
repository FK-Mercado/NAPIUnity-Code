using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "RadiantJudgment", menuName = "NAPI/Status Effects/DOT/Juicio Radiante (Lux)")]
    public class RadiantJudgmentData : DamageOverTimeEffectData
    {
        [Header("Extra: reducción de esquiva")]
        [Range(0f, 1f)] public float evasionReduction = 0.15f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            int damagePerTurn = DamageScalingUtility.Calculate(scalingType, scalingValue, source, target, damageResult);
            return new RadiantJudgmentEffect(duration, damagePerTurn, evasionReduction, effectName);
        }
    }
}
