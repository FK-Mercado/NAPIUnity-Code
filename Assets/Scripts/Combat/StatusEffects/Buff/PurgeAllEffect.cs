using UnityEngine;

namespace NAPI.Combat
{
    public class PurgeAllEffect : StatusEffect
    {
        public PurgeAllEffect(int duration) : base(duration)
        {
            EffectName = "Purga Total";
        }

        public override void OnApply(Combatant target)
        {
            target.PurgeAll();
            Debug.Log($"{target.Data.displayName} queda en blanco: sin buffs ni debuffs.");
        }
    }
}
