using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "ParalyzingArcBuff", menuName = "NAPI/Status Effects/Buff/Paralyzing Arc")]
    public class ParalyzingArcBuffData : StatusEffectData
    {
        [Header("Arco Paralizante")]
        [Range(0f, 1f)] public float procChance = 0.25f;
        [Tooltip("Duración de la Parálisis que provoca, no de este buff.")]
        public int paralysisDuration = 1;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new ParalyzingArcEffect(duration, procChance, paralysisDuration);
        }
    }
}
