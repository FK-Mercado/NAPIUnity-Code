using UnityEngine;

namespace NAPI.Combat
{
    public class PurgeBuffsEffect : StatusEffect
    {
        public PurgeBuffsEffect(int duration) : base(duration)
        {
            EffectName = "Purga";
        }

        public override void OnApply(Combatant target)
        {
            target.RemoveAllBuffs();
            Debug.Log($"{target.Data.displayName} pierde todos sus buffs.");
        }
    }
}
