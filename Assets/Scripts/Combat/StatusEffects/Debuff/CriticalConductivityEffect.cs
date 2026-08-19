using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class CriticalConductivityEffect : StatusEffect
    {
        private readonly float bonusCritChance;

        public CriticalConductivityEffect(int duration, float bonusCritChance) : base(duration)
        {
            EffectName = "Conductividad Crítica";
            this.bonusCritChance = bonusCritChance;
        }

        public override void OnApply(Combatant target)
        {
            target.AddElementalCritVulnerability(ElementType.aqua, bonusCritChance);
            target.AddElementalCritVulnerability(ElementType.Natura, bonusCritChance);
            Debug.Log($"{target.Data.displayName} queda cargado estáticamente: vulnerable a críticos de Agua y Natura.");
        }

        public override void OnRemove(Combatant target)
        {
            target.RemoveElementalCritVulnerability(ElementType.aqua, bonusCritChance);
            target.RemoveElementalCritVulnerability(ElementType.Natura, bonusCritChance);
        }
    }
}
