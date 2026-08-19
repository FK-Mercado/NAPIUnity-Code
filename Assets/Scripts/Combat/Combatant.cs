using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class Combatant
    {
        public CombatantDataBase Data { get; private set; }
        public CharacterProgression Progression { get; private set; }
        public bool IsPlayerControlled { get; private set; }

        public int MaxHP { get; private set; }
        public int MaxEnergy { get; private set; }

        #region //--------------Base Stats-----------------
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Speed { get; private set; }
        #endregion

        #region //--------------Buff Stats-----------------
        private int attackModifier;
        private int defenseModifier;
        private int speedModifier;
        #endregion

        #region //--------------Total Stats-----------------
        public int FinalAttack => Attack + attackModifier;
        public int FinalDefense => Defense + defenseModifier;
        public int FinalSpeed => Speed + speedModifier;
        #endregion

        public int CurrentHP { get; private set; }
        public int CurrentEnergy { get; private set; }
        public int UltimateCharge { get; private set; }

        public int BreakGauge { get; private set; }
        public int MaxBreakGauge { get; private set; } = 100;
        public bool IsBroken => BreakGauge >= MaxBreakGauge;

        private readonly List<StatusEffect> statusEffects = new();
        public IReadOnlyList<StatusEffect> StatusEffects => statusEffects;

        public float NextTurnValue { get; set; }
        public bool IsAlive => CurrentHP > 0;

        // ---- Nuevo: crítico elemental ----
        public const float BaseCritChance = 0.05f;
        public const float CritDamageMultiplier = 1.5f;
        private float critChanceModifier;
        private readonly Dictionary<ElementType, float> elementalCritVulnerability = new();

        // ---- Nuevo: costo de energía y regeneración ----
        private float energyCostMultiplier = 1f;
        public float EnergyCostMultiplier => energyCostMultiplier;

        private float energyRegenMultiplier = 1f;
        public float EnergyRegenMultiplier => energyRegenMultiplier;

        // ---- Nuevo: modificador general de esquiva (lo usa Manto de
        // Penumbra como base, y Juicio Radiante lo reduce) ----
        private float evasionModifier;
        public float EvasionModifier => evasionModifier;

        public Combatant(CombatantDataBase data, bool isPlayerControlled, CharacterProgression progression = null)
        {
            Data = data;
            Progression = progression;
            IsPlayerControlled = isPlayerControlled;

            if (progression != null)
            {
                MaxHP = progression.GetMaxHP();
                MaxEnergy = progression.GetMaxEnergy();
                Attack = progression.GetAttack();
                Defense = progression.GetDefense();
                Speed = progression.GetSpeed();
            }
            else
            {
                MaxHP = data.maxHP;
                MaxEnergy = data.maxEnergy;
                Attack = data.attack;
                Defense = data.defense;
                Speed = data.speed;
            }

            CurrentHP = MaxHP;
            CurrentEnergy = MaxEnergy / 2;
            UltimateCharge = 0;
            attackModifier = 0;
            defenseModifier = 0;
            speedModifier = 0;
            BreakGauge = 0;
            NextTurnValue = 0f;
        }

        public void AddBreak(int amount)
        {
            BreakGauge = Mathf.Min(MaxBreakGauge, BreakGauge + amount);
        }

        public void RecoverBreak()
        {
            BreakGauge = 0;
        }

        public void AddStatusEffect(StatusEffect effect)
        {
            Debug.Log($"Añadiendo efecto: {effect.EffectName} a {Data.displayName}");
            statusEffects.Add(effect);
            effect.OnApply(this);
        }

        public void ProcessTurnStartEffects()
        {
            foreach (StatusEffect effect in statusEffects)
            {
                effect.OnTurnStart(this);
            }
        }

        public void ProcessTurnEndEffects()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                StatusEffect effect = statusEffects[i];
                effect.OnTurnEnd(this);

                if (effect.IsExpired)
                {
                    Debug.Log($"{Data.displayName} pierde {effect.EffectName}");
                    effect.OnRemove(this);
                    statusEffects.RemoveAt(i);
                }
            }
        }

        public void TakeDamage(int amount)
        {
            CurrentHP = Mathf.Max(0, CurrentHP - amount);
            GainUltimateCharge(10);
        }

        public void Heal(int amount)
        {
            CurrentHP = Mathf.Min(MaxHP, CurrentHP + amount);
        }

        public bool CanUseSkill(SkillData skill)
        {
            if (skill.skillType == SkillType.Definitivo)
                return CurrentEnergy >= MaxEnergy / 2 && UltimateCharge >= 100;

            return CurrentEnergy >= Mathf.RoundToInt(skill.energyCost * energyCostMultiplier);
        }

        public void SpendEnergy(int amount)
        {
            CurrentEnergy = Mathf.Max(0, CurrentEnergy - amount);
        }

        public void GainEnergy(int amount)
        {
            CurrentEnergy = Mathf.Min(MaxEnergy, CurrentEnergy + amount);
        }

        public void GainUltimateCharge(int amount)
        {
            UltimateCharge = Mathf.Min(100, UltimateCharge + amount);
        }

        public void ResetUltimateChargeAfterUse()
        {
            UltimateCharge = 0;
        }

        public void ApplyGuard()
        {
            GainEnergy(Mathf.RoundToInt(MaxEnergy * 0.25f));
            Heal(Mathf.RoundToInt(MaxHP * 0.15f));
        }

        // ---- Modificadores de stat (Buff/Debuff) ----
        public void ModifyAttack(int amount) => attackModifier += amount;
        public void ModifyDefense(int amount) => defenseModifier += amount;
        public void ModifySpeed(int amount) => speedModifier += amount;

        public void IncreaseMaxHP(int amount)
        {
            MaxHP += amount;
            CurrentHP += amount;
        }

        public void DecreaseMaxHP(int amount)
        {
            MaxHP = Mathf.Max(1, MaxHP - amount);
            CurrentHP = Mathf.Min(CurrentHP, MaxHP);
        }

        public void IncreaseMaxEnergy(int amount)
        {
            MaxEnergy += amount;
            CurrentEnergy += amount;
        }

        public void DecreaseMaxEnergy(int amount)
        {
            MaxEnergy = Mathf.Max(1, MaxEnergy - amount);
            CurrentEnergy = Mathf.Min(CurrentEnergy, MaxEnergy);
        }

        public void ModifyEnergyCostMultiplier(float amount) => energyCostMultiplier = Mathf.Max(0f, energyCostMultiplier + amount);
        public void ModifyEnergyRegenMultiplier(float amount) => energyRegenMultiplier = Mathf.Max(0f, energyRegenMultiplier + amount);
        public void ModifyEvasion(float amount) => evasionModifier += amount;

        /// <summary>Nivel de una skill para ESTE combatiente. Un enemigo
        /// (sin Progression) siempre devuelve 1: sus skills no suben de
        /// nivel, se ajustan directo en el EnemyData si hace falta.</summary>
        public int GetSkillLevel(SkillData skill) => Progression != null ? Progression.GetSkillLevel(skill) : 1;

        /// <summary>
        /// Saca todos los efectos activos marcados como IsDebuff = true,
        /// llamando su OnRemove (revierte modificadores, restaura MaxHP,
        /// etc.) antes de sacarlos de la lista. Buffs y DOT no se tocan.
        /// </summary>
        public void RemoveAllDebuffs()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                if (!statusEffects[i].IsDebuff) continue;

                Debug.Log($"{Data.displayName} se libera de {statusEffects[i].EffectName}.");
                statusEffects[i].OnRemove(this);
                statusEffects.RemoveAt(i);
            }
        }

        /// <summary>Igual que RemoveAllDebuffs pero para IsBuff = true. DOT no se toca.</summary>
        public void RemoveAllBuffs()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                if (!statusEffects[i].IsBuff) continue;

                Debug.Log($"{Data.displayName} pierde {statusEffects[i].EffectName}.");
                statusEffects[i].OnRemove(this);
                statusEffects.RemoveAt(i);
            }
        }

        /// <summary>Buffs y debuffs afuera, en ese orden. DOT sigue sin tocarse.</summary>
        public void PurgeAll()
        {
            RemoveAllBuffs();
            RemoveAllDebuffs();
        }

        // ---- Crítico elemental ----
        public float GetCritChance(ElementType incomingElement)
        {
            float bonus = elementalCritVulnerability.TryGetValue(incomingElement, out float v) ? v : 0f;
            return Mathf.Clamp01(BaseCritChance + critChanceModifier + bonus);
        }

        public void AddElementalCritVulnerability(ElementType element, float amount)
        {
            elementalCritVulnerability[element] = (elementalCritVulnerability.TryGetValue(element, out float v) ? v : 0f) + amount;
        }

        public void RemoveElementalCritVulnerability(ElementType element, float amount)
        {
            if (!elementalCritVulnerability.ContainsKey(element)) return;
            elementalCritVulnerability[element] -= amount;
            if (elementalCritVulnerability[element] <= 0f)
                elementalCritVulnerability.Remove(element);
        }

        // ---- Disparadores de los hooks de StatusEffect ----

        /// <summary>Se consulta en TurnManager antes de ejecutar la acción del turno.</summary>
        public bool ShouldSkipTurn()
        {
            return statusEffects.Any(e => e.ShouldSkipHolderTurn(this));
        }

        /// <summary>Se llama desde SkillExecutor después de restar el daño al objetivo.</summary>
        public void NotifyDamageDealt(Combatant target, SkillData skill, int damageDealt, bool wasCrit)
        {
            foreach (var effect in statusEffects.ToArray())
                effect.OnDamageDealtByHolder(this, target, skill, damageDealt, wasCrit);
        }

        /// <summary>Se llama desde SkillExecutor cada vez que este combatiente usa una skill.</summary>
        public void NotifySkillUsed(SkillData skill)
        {
            foreach (var effect in statusEffects.ToArray())
                effect.OnHolderUsedSkill(this, skill);
        }

        /// <summary>Deja que los efectos DEL ATACANTE fuercen fallos, etc.</summary>
        public int ModifyOutgoingDamage(Combatant target, SkillData skill, int rawDamage)
        {
            int modified = rawDamage;
            foreach (var effect in statusEffects.ToArray())
                modified = effect.ModifyOutgoingDamage(this, target, skill, modified);
            return Mathf.Max(0, modified);
        }

        /// <summary>Deja que los efectos DEL OBJETIVO reduzcan/esquiven el golpe.</summary>
        public int ModifyIncomingDamage(Combatant attacker, SkillData incomingSkill, int incomingDamage)
        {
            int modified = incomingDamage;
            foreach (var effect in statusEffects.ToArray())
                modified = effect.ModifyIncomingDamage(this, attacker, incomingSkill, modified);
            return Mathf.Max(0, modified);
        }
    }
}
