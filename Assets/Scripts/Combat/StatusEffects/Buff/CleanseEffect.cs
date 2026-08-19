using UnityEngine;

namespace NAPI.Combat
{
    public class CleanseEffect : StatusEffect
    {
        public CleanseEffect(int duration) : base(duration)
        {
            EffectName = "Purificación";
        }

        public override bool IsBuff => true;

        public override void OnApply(Combatant target)
        {
            target.RemoveAllDebuffs();
            Debug.Log($"{target.Data.displayName} se purifica de todos los debuffs.");
        }
    }
}
