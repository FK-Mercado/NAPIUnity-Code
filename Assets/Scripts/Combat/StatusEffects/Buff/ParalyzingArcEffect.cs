using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class ParalyzingArcEffect : StatusEffect
    {
        private readonly float procChance;
        private readonly int paralysisDuration;

        public ParalyzingArcEffect(int duration, float procChance, int paralysisDuration) : base(duration)
        {
            EffectName = "Arco Paralizante";
            this.procChance = procChance;
            this.paralysisDuration = paralysisDuration;
        }

        // Solo procs con ataques de elemento Rayo.
        public override void OnDamageDealtByHolder(Combatant holder, Combatant target, SkillData skill, int damageDealt, bool wasCrit)
        {
            if (skill.element != ElementType.Rayo) return;
            if (Random.value >= procChance) return;

            target.AddStatusEffect(new ParalysisEffect(paralysisDuration));
            Debug.Log($"{target.Data.displayName} queda paralizado por el Arco Paralizante.");
        }
    }
}
