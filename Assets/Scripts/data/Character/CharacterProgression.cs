using System;
using System.Collections.Generic;
using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class CharacterProgression
    {
        public CharacterData Data { get; private set; }
        public LevelRankConfig Config { get; private set; }

        public int Level { get; private set; } = 1;
        public int CurrentXP { get; private set; } = 0;
        public int UnlockedRankIndex { get; private set; } = 0;

        // Nivel de cada skill, clave = SkillData.id. Si una skill no está
        // en el diccionario, se asume nivel 1 (recién adquirida).
        private readonly Dictionary<string, int> skillLevels = new();

        public CharacterProgression(CharacterData data, LevelRankConfig config)
        {
            Data = data;
            Config = config;
        }

        public int CurrentLevelCap => Config.rankLevelCaps[Mathf.Min(UnlockedRankIndex, Config.rankLevelCaps.Length - 1)];
        public bool IsAtLevelCap => Level >= CurrentLevelCap;
        public bool IsMaxLevel => Level >= 100;

        public void AddXP(int amount)
        {
            if (IsMaxLevel) return;
            CurrentXP += amount;

            while (!IsAtLevelCap && CurrentXP >= Config.GetXPRequired(Level))
            {
                CurrentXP -= Config.GetXPRequired(Level);
                Level++;
            }
        }

        public bool TryBreakRankLimit(int availableCurrency, Func<ItemData, int, bool> trySpendResource)
        {
            if (UnlockedRankIndex >= Config.rankBreakCosts.Length) return false;
            if (!IsAtLevelCap) return false;

            var cost = Config.rankBreakCosts[UnlockedRankIndex];
            if (availableCurrency < cost.currencyCost) return false;
            if (!trySpendResource(cost.requiredResource, cost.resourceAmount)) return false;

            UnlockedRankIndex++;
            return true;
        }

        public void SetState(int level, int currentXP, int unlockedRankIndex)
        {
            Level = Mathf.Clamp(level, 1, 100);
            CurrentXP = Mathf.Max(0, currentXP);
            UnlockedRankIndex = Mathf.Clamp(unlockedRankIndex, 0, Config.rankLevelCaps.Length - 1);
        }

        public int GetMaxHP() => ScaleStat(Data.maxHP, Data.hpGrowthPerLevel);
        public int GetMaxEnergy() => ScaleStat(Data.maxEnergy, Data.energyGrowthPerLevel);
        public int GetAttack() => ScaleStat(Data.attack, Data.attackGrowthPerLevel);
        public int GetDefense() => ScaleStat(Data.defense, Data.defenseGrowthPerLevel);
        public int GetSpeed() => ScaleStat(Data.speed, Data.speedGrowthPerLevel);

        private int ScaleStat(float baseValue, float growthPerLevel)
        {
            float raw = baseValue + growthPerLevel * (Level - 1);
            return Mathf.RoundToInt(raw * Config.GetRarityMultiplier(Data.rarity));
        }

        // ---- Nivel de skills ----

        public int GetSkillLevel(SkillData skill)
        {
            if (skill == null) return 1;
            return skillLevels.TryGetValue(skill.id, out int level) ? level : 1;
        }

        /// <summary>
        /// Fija el nivel de una skill directamente (carga de guardado o
        /// pruebas), igual que SetState para el nivel de personaje.
        /// </summary>
        public void SetSkillLevel(SkillData skill, int level)
        {
            if (skill == null) return;
            skillLevels[skill.id] = Mathf.Clamp(level, 1, skill.maxSkillLevel);
        }

        /// <summary>
        /// Sube la skill un nivel si hay currency/recurso suficiente. El
        /// costo escala con el nivel actual (currencyCostPerLevel * nivel
        /// actual), igual de simple que el resto del sistema de progresión.
        /// </summary>
        public bool TryLevelUpSkill(SkillData skill, int availableCurrency, Func<ItemData, int, bool> trySpendResource)
        {
            if (skill == null) return false;

            int currentLevel = GetSkillLevel(skill);
            if (currentLevel >= skill.maxSkillLevel) return false;

            int currencyNeeded = skill.currencyCostPerLevel * currentLevel;
            if (availableCurrency < currencyNeeded) return false;

            if (skill.levelUpResource != null &&
                !trySpendResource(skill.levelUpResource, skill.resourceCostPerLevel))
                return false;

            skillLevels[skill.id] = currentLevel + 1;
            return true;
        }
    }
}
