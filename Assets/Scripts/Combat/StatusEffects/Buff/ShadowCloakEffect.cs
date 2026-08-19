using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    /// <summary>
    /// Nota de diseño: hoy no existe un sistema de "prioridad de
    /// objetivo" (el TurnManager ataca siempre al primer vivo de la
    /// lista). "Menos probable que lo ataquen" se aproxima acá como una
    /// probabilidad de esquivar el golpe por completo una vez que ya fue
    /// elegido como objetivo. Si más adelante armás selección de
    /// objetivo por prioridad, este mismo efecto podría además restar
    /// peso en esa selección.
    /// </summary>
    public class ShadowCloakEffect : StatusEffect
    {
        private readonly float evadeChance;

        public ShadowCloakEffect(int duration, float evadeChance) : base(duration)
        {
            EffectName = "Manto de Penumbra";
            this.evadeChance = evadeChance;
        }

        public override int ModifyIncomingDamage(Combatant holder, Combatant attacker, SkillData incomingSkill, int incomingDamage)
        {
            float effectiveChance = Mathf.Clamp01(evadeChance + holder.EvasionModifier);

            if (Random.value < effectiveChance)
            {
                Debug.Log($"{holder.Data.displayName} se funde en las sombras y evita el ataque.");
                return 0;
            }

            return incomingDamage;
        }
    }
}
