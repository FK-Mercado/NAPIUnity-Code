using System.Collections.Generic;
using NAPI.Data;

namespace NAPI.Combat
{
    /// <summary>
    /// Contexto de datos necesario para ejecutar una SkillData.
    ///
    /// Es solamente un contenedor: no ejecuta la skill, no calcula daño,
    /// no aplica efectos, no gestiona CAR, no evalúa condiciones de
    /// Ultimate y no conoce turnos ni UI. Esas responsabilidades siguen
    /// perteneciendo a SkillExecutor (todavía sin modificar en esta etapa).
    ///
    /// Pensado para que, en una etapa posterior, SkillExecutor pueda
    /// recibir un contexto común tanto para una skill normal como para
    /// una skill proveniente de un UltimateLaunchPoint, sin duplicar la
    /// forma en que se agrupan attacker/skill/target(s).
    /// </summary>
    public class SkillExecutionContext
    {
        public Combatant Attacker { get; }
        public SkillData Skill { get; }

        /// <summary>Objetivo principal, para skills de un solo objetivo.</summary>
        public Combatant Target { get; }

        /// <summary>Objetivos múltiples, para skills de equipo (ej. apoyo a todos los aliados).</summary>
        public IReadOnlyList<Combatant> Targets { get; }

        public SkillExecutionContext(Combatant attacker, SkillData skill, Combatant target)
        {
            Attacker = attacker;
            Skill = skill;
            Target = target;
            Targets = null;
        }

        public SkillExecutionContext(Combatant attacker, SkillData skill, IReadOnlyList<Combatant> targets)
        {
            Attacker = attacker;
            Skill = skill;
            Target = null;
            Targets = targets;
        }
    }
}
