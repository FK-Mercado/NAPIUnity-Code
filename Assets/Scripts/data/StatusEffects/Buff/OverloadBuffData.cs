using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    [System.Flags]
    public enum OverloadStat
    {
        None = 0,
        Attack = 1 << 0,
        Defense = 1 << 1,
        Speed = 1 << 2,
        MaxHP = 1 << 3
    }

    [CreateAssetMenu(fileName = "OverloadBuff", menuName = "NAPI/Status Effects/Buff/Overload (Multi-Stat)")]
    public class OverloadBuffData : StatusEffectData
    {
        [Header("Sobrecarga: qué estadísticas afecta")]
        public OverloadStat enabledStats = OverloadStat.Speed;

        [Header("Fatiga (debuff al terminar)")]
        public int fatigueDuration = 2;

        [Header("Ataque — % buff / % fatiga")]
        [Range(0f, 3f)] public float attackBoostPercentage = 1f;
        [Range(0f, 1f)] public float attackFatiguePercentage = 0.6f;

        [Header("Defensa — % buff / % fatiga")]
        [Range(0f, 3f)] public float defenseBoostPercentage = 1f;
        [Range(0f, 1f)] public float defenseFatiguePercentage = 0.6f;

        [Header("Velocidad — % buff / % fatiga")]
        [Range(0f, 3f)] public float speedBoostPercentage = 1f;
        [Range(0f, 1f)] public float speedFatiguePercentage = 0.6f;

        [Header("Vida Máxima — % buff / % fatiga")]
        [Range(0f, 3f)] public float maxHPBoostPercentage = 1f;
        [Range(0f, 1f)] public float maxHPFatiguePercentage = 0.6f;

        public override StatusEffect CreateEffect(Combatant source, Combatant target, SkillData skill, DamageResult damageResult)
        {
            StatAmounts boost = new StatAmounts(
                Has(OverloadStat.Attack) ? Mathf.RoundToInt(target.Attack * attackBoostPercentage) : 0,
                Has(OverloadStat.Defense) ? Mathf.RoundToInt(target.Defense * defenseBoostPercentage) : 0,
                Has(OverloadStat.Speed) ? Mathf.RoundToInt(target.Speed * speedBoostPercentage) : 0,
                Has(OverloadStat.MaxHP) ? Mathf.RoundToInt(target.MaxHP * maxHPBoostPercentage) : 0
            );

            StatAmounts fatigue = new StatAmounts(
                Has(OverloadStat.Attack) ? Mathf.RoundToInt(target.Attack * attackFatiguePercentage) : 0,
                Has(OverloadStat.Defense) ? Mathf.RoundToInt(target.Defense * defenseFatiguePercentage) : 0,
                Has(OverloadStat.Speed) ? Mathf.RoundToInt(target.Speed * speedFatiguePercentage) : 0,
                Has(OverloadStat.MaxHP) ? Mathf.RoundToInt(target.MaxHP * maxHPFatiguePercentage) : 0
            );

            return new OverloadEffect(duration, enabledStats, boost, fatigueDuration, fatigue);
        }

        private bool Has(OverloadStat flag) => (enabledStats & flag) != 0;
    }
}
