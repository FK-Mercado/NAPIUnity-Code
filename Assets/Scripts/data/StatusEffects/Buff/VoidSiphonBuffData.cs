using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "VoidSiphonBuff", menuName = "NAPI/Status Effects/Buff/Void Siphon")]
    public class VoidSiphonBuffData : StatusEffectData
    {
        [Header("Sifón del Vacío")]
        [Tooltip("Fracción del daño infligido que se absorbe. La mitad se convierte en HP y la otra mitad en EN.")]
        [Range(0f, 1f)] public float siphonPercentage = 0.2f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new VoidSiphonEffect(duration, siphonPercentage);
        }
    }
}
