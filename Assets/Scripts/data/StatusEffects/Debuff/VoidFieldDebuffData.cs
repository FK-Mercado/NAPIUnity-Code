using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "VoidFieldDebuff", menuName = "NAPI/Status Effects/Debuff/Void Field")]
    public class VoidFieldDebuffData : StatusEffectData
    {
        [Header("Campo de Vacío")]
        [Tooltip("Porcentaje del propio MaxHP que el afectado pierde cada vez que golpea cuerpo a cuerpo.")]
        [Range(0f, 1f)] public float recoilPercentage = 0.1f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new VoidFieldEffect(duration, recoilPercentage);
        }
    }
}
