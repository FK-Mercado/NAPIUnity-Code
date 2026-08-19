using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "SoftenDefenseDebuff", menuName = "NAPI/Status Effects/Debuff/Soften Defense")]
    public class SoftenDefenseDebuffData : StatusEffectData
    {
        [Header("Soften Defense")]
        [Tooltip("Amplificación del daño (ej. 0.3 = +30%) cuando el golpe es cuerpo a cuerpo o pega la debilidad elemental del objetivo.")]
        [Range(0f, 2f)] public float damageAmplification = 0.3f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new SoftenDefenseEffect(duration, damageAmplification);
        }
    }
}
