using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class FluidBarrierEffect : StatusEffect
    {
        private readonly float meleeReduction;
        private readonly float deflectChance;

        public FluidBarrierEffect(int duration, float meleeReduction, float deflectChance) : base(duration)
        {
            EffectName = "Barrera de Fluido";
            this.meleeReduction = meleeReduction;
            this.deflectChance = deflectChance;
        }

        public override int ModifyIncomingDamage(Combatant holder, Combatant attacker, SkillData incomingSkill, int incomingDamage)
        {
            if (incomingSkill.attackRange == AttackRangeType.Melee)
            {
                int reduced = Mathf.RoundToInt(incomingDamage * (1f - meleeReduction));
                Debug.Log($"{holder.Data.displayName} absorbe parte del golpe con la Barrera de Fluido.");
                return reduced;
            }

            if (incomingSkill.attackRange == AttackRangeType.Ranged && Random.value < deflectChance)
            {
                Debug.Log($"{holder.Data.displayName} desvía el proyectil con la Barrera de Fluido.");
                return 0;
            }

            return incomingDamage;
        }
    }
}
