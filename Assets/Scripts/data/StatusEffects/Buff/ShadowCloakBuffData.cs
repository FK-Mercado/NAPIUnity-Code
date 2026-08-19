using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "ShadowCloakBuff", menuName = "NAPI/Status Effects/Buff/Shadow Cloak")]
    public class ShadowCloakBuffData : StatusEffectData
    {
        [Header("Manto de Penumbra")]
        [Tooltip("Probabilidad base de evitar por completo un golpe entrante mientras dure.")]
        [Range(0f, 1f)] public float evadeChance = 0.3f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            return new ShadowCloakEffect(duration, evadeChance);
        }
    }
}
