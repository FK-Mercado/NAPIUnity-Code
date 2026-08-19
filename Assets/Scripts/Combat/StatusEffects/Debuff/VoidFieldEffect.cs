using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class VoidFieldEffect : StatusEffect
    {
        private readonly float recoilPercentage;

        public VoidFieldEffect(int duration, float recoilPercentage) : base(duration)
        {
            EffectName = "Campo de Vacío";
            this.recoilPercentage = recoilPercentage;
        }

        // El afectado usa una skill a distancia: el golpe se anula del todo.
        public override int ModifyOutgoingDamage(Combatant holder, Combatant target, SkillData skill, int outgoingDamage)
        {
            if (skill.attackRange == AttackRangeType.Ranged)
            {
                Debug.Log($"{holder.Data.displayName} no puede acertar ataques a distancia bajo el Campo de Vacío.");
                return 0;
            }

            return outgoingDamage;
        }

        // El afectado usa una skill cuerpo a cuerpo: recibe daño propio,
        // haya acertado o no (por eso está en OnHolderUsedSkill y no en
        // OnDamageDealtByHolder).
        public override void OnHolderUsedSkill(Combatant holder, SkillData skill)
        {
            if (skill.attackRange != AttackRangeType.Melee) return;

            int recoil = Mathf.RoundToInt(holder.MaxHP * recoilPercentage);
            holder.TakeDamage(recoil);
            Debug.Log($"{holder.Data.displayName} recibe {recoil} de daño por el retroceso del Vacío.");
        }
    }
}
