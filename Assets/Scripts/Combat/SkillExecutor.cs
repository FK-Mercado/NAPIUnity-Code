using System.Collections.Generic;
using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public static class SkillExecutor
    {
        /// <summary>
        /// Wrapper delgado: conserva la firma actual (sin eventBus) para que
        /// TurnManager y cualquier otro llamador existente sigan compilando
        /// y funcionando sin cambios. Arma un SkillExecutionContext y delega
        /// a la ejecución real. Sin bus, no se publica ningún evento.
        /// </summary>
        public static void Execute(
            Combatant attacker,
            Combatant target,
            SkillData skill,
            CombatEventBus eventBus = null)
        {
            var context = new SkillExecutionContext(attacker, skill, target);
            Execute(context, eventBus);
        }

        /// <summary>
        /// Punto común real de ejecución de una skill. Pensado para que,
        /// en etapas futuras, UltimateLaunchPoint pueda construir su propio
        /// SkillExecutionContext y llegar hasta acá sin duplicar esta
        /// lógica. Si eventBus es null, se comporta exactamente igual que
        /// antes (no publica nada).
        /// </summary>
        public static void Execute(SkillExecutionContext context, CombatEventBus eventBus = null)
        {
            Combatant attacker = context.Attacker;
            Combatant target = context.Target;
            SkillData skill = context.Skill;

            Debug.Log($"{attacker.Data.displayName} usa {skill.skillName}");

            // Costo de energía: antes no se descontaba en ningún lado.
            // Se agrega acá, ya multiplicado por el multiplicador del
            // atacante (lo usa Combustión de Energía para duplicarlo).
            int energySpent = Mathf.RoundToInt(skill.energyCost * attacker.EnergyCostMultiplier);
            attacker.SpendEnergy(energySpent);

            if (eventBus != null)
                eventBus.Publish(new EnergySpentEvent(attacker, energySpent));

            DamageResult result = DamageCalculator.CalculateDamage(attacker, target, skill);

            // Efectos del ATACANTE que pueden anular el golpe antes de
            // que llegue (Campo de Vacío contra ataques a distancia).
            result.Damage = attacker.ModifyOutgoingDamage(target, skill, result.Damage);

            target.TakeDamage(result.Damage);

            // Efectos que reaccionan a "usé una skill" sin importar si
            // pegó o no (retroceso de Campo de Vacío en cuerpo a cuerpo).
            attacker.NotifySkillUsed(skill);

            if (eventBus != null)
                eventBus.Publish(new SkillUsedEvent(attacker, skill));

            // Efectos que reaccionan a "repartí daño" (Sifón del Vacío,
            // Arco Paralizante, retroceso de Combustión de Energía).
            attacker.NotifyDamageDealt(target, skill, result.Damage, result.IsCrit);

            if (eventBus != null)
            {
                eventBus.Publish(new DamageDealtEvent(attacker, target, result));
                eventBus.Publish(new DamageReceivedEvent(target, attacker, result));

                if (result.IsCrit)
                    eventBus.Publish(new CriticalHitEvent(attacker, target, result));
            }

            if (result.HitWeakness)
            {
                target.AddBreak(50);
                Debug.Log($"{target.Data.displayName} acumula Break: {target.BreakGauge}/{target.MaxBreakGauge}");
            }

            ApplyEffects(skill.appliedStatusEffects, attacker, target, skill, result);
            ApplyEffects(skill.selfAppliedStatusEffects, attacker, attacker, skill, result);
        }

        /// <summary>
        /// Para skills de apoyo a todo el equipo (ej. Aura de Bastión):
        /// aplica selfAppliedStatusEffects a cada aliado vivo, no solo a
        /// quien la usó. No pasa por DamageCalculator porque no hay
        /// objetivo enemigo ni daño involucrado.
        /// </summary>
        public static void ExecuteTeamSupport(Combatant source, IEnumerable<Combatant> allies, SkillData skill)
        {
            Debug.Log($"{source.Data.displayName} usa {skill.skillName} (apoyo de equipo)");

            source.SpendEnergy(Mathf.RoundToInt(skill.energyCost * source.EnergyCostMultiplier));

            if (skill.selfAppliedStatusEffects == null) return;

            foreach (var ally in allies)
            {
                if (!ally.IsAlive) continue;
                ApplyEffects(skill.selfAppliedStatusEffects, source, ally, skill, default);
            }
        }

        private static void ApplyEffects(
            StatusEffectData[] effects,
            Combatant source,
            Combatant receiver,
            SkillData skill,
            DamageResult result)
        {
            if (effects == null) return;

            foreach (var effectData in effects)
            {
                if (effectData == null) continue;

                receiver.AddStatusEffect(effectData.CreateEffect(source, receiver, skill, result));
                Debug.Log($"{receiver.Data.displayName} recibe {effectData.effectName}");
            }
        }
    }
}