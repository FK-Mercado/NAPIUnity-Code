using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "FluidBarrierBuff", menuName = "NAPI/Status Effects/Buff/Fluid Barrier")]
    public class FluidBarrierBuffData : StatusEffectData
    {
        [Header("Barrera de Fluido")]
        [Tooltip("Porcentaje del daño cuerpo a cuerpo entrante que se absorbe.")]
        [Range(0f, 1f)] public float meleeDamageReduction = 0.4f;
        [Tooltip("Probabilidad de desviar por completo un ataque a distancia.")]
        [Range(0f, 1f)] public float rangedDeflectChance = 0.3f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new FluidBarrierEffect(duration, meleeDamageReduction, rangedDeflectChance);
        }
    }
}
