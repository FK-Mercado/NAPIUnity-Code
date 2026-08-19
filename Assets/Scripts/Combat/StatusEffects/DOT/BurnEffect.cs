using UnityEngine;

namespace NAPI.Combat
{
    public class BurnEffect : StatusEffect
    {
        private readonly int damagePerTurn;

        public BurnEffect(
            int duration,
            int damage)
            : base(duration)
        {
            EffectName = "Burn";

            damagePerTurn = damage;
        }

        public override void OnTurnStart(
            Combatant target)
        {
            target.TakeDamage(
                damagePerTurn);

            Debug.Log(
                $"{target.Data.displayName} recibe {damagePerTurn} de quemadura.");
        }
    }
}