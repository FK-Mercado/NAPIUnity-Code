using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class VoidSiphonEffect : StatusEffect
    {
        private readonly float percentage;

        public VoidSiphonEffect(int duration, float percentage) : base(duration)
        {
            EffectName = "Sifón del Vacío";
            this.percentage = percentage;
        }

        public override void OnDamageDealtByHolder(Combatant holder, Combatant target, SkillData skill, int damageDealt, bool wasCrit)
        {
            int absorbed = Mathf.RoundToInt(damageDealt * percentage);
            int hpHalf = absorbed / 2;
            int energyHalf = absorbed - hpHalf;

            holder.Heal(hpHalf);
            holder.GainEnergy(energyHalf);

            Debug.Log($"{holder.Data.displayName} absorbe {absorbed} del Vacío ({hpHalf} HP / {energyHalf} EN).");
        }
    }
}
