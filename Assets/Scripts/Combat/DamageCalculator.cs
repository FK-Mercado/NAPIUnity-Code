using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public static class DamageCalculator
    {
        public static DamageResult CalculateDamage(
            Combatant attacker,
            Combatant target,
            SkillData skill)
        {
            DamageResult result = new DamageResult();

            // El nivel de la skill (1 si el atacante es un enemigo, o un
            // PJ que todavía no la subió) mueve el rango de daño hacia
            // arriba antes de tirar el random.
            int skillLevel = attacker.GetSkillLevel(skill);
            float levelBonus = skill.damagePercentagePerLevel * (skillLevel - 1);
            float minPercentage = skill.minDamagePercentage + levelBonus;
            float maxPercentage = skill.maxDamagePercentage + levelBonus;

            int totalDamage = 0;

            for (int i = 0; i < skill.numberOfHits; i++)
            {
                float rolledPercentage = Random.Range(minPercentage, maxPercentage);

                int rawDamage = Mathf.RoundToInt(attacker.FinalAttack * rolledPercentage);

                int hitDamage = rawDamage - target.FinalDefense / 2;
                hitDamage = Mathf.Max(1, hitDamage);

                Debug.Log($"Hit {i + 1}: {hitDamage} damage ({rolledPercentage:P0} ATK, skill nivel {skillLevel})");
                totalDamage += hitDamage;
            }

            bool hitsWeakness = skill.element == target.Data.weakness;

            if (hitsWeakness)
            {
                totalDamage = Mathf.RoundToInt(totalDamage * 1.5f);
                Debug.Log($"Total DMG: {totalDamage} (Weakness)");
            }

            bool isCrit = Random.value < target.GetCritChance(skill.element);
            if (isCrit)
            {
                totalDamage = Mathf.RoundToInt(totalDamage * Combatant.CritDamageMultiplier);
                Debug.Log($"Total DMG: {totalDamage} (Crítico)");
            }

            totalDamage = target.ModifyIncomingDamage(attacker, skill, totalDamage);

            result.Damage = totalDamage;
            result.HitWeakness = hitsWeakness;
            result.IsCrit = isCrit;

            return result;
        }
    }
}
