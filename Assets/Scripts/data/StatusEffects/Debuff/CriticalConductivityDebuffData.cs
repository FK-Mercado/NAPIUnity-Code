using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "CriticalConductivityDebuff", menuName = "NAPI/Status Effects/Debuff/Critical Conductivity")]
    public class CriticalConductivityDebuffData : StatusEffectData
    {
        [Header("Conductividad Crítica")]
        [Tooltip("Probabilidad de crítico ADICIONAL contra ataques de Agua o Natura mientras dure.")]
        [Range(0f, 1f)] public float bonusCritChance = 0.5f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new CriticalConductivityEffect(duration, bonusCritChance);
        }
    }
}
