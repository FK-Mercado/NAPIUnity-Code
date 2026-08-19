using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    public abstract class StatusEffectData : ScriptableObject
    {
        [Header("Info")]
        public string effectName;
        [TextArea]
        public string description;

        [Header("Visual")]
        public Sprite icon;

        [Header("Duration")]
        public int duration = 1;

        public abstract StatusEffect CreateEffect(
            Combatant source,
            Combatant target,
            SkillData skill,
            DamageResult damageResult
        );
    }
}