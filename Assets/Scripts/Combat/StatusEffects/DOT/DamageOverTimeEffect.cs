using UnityEngine;

namespace NAPI.Combat
{
    /// <summary>
    /// Runtime genérico para cualquier daño por turno. Reemplaza a
    /// BurnEffect: la única diferencia entre "Quemadura", "Veneno",
    /// "Escarcha", etc. era el nombre y de dónde salía el número de
    /// daño — ambas cosas ya vienen resueltas desde
    /// DamageOverTimeEffectData antes de llegar acá.
    /// </summary>
    public class DamageOverTimeEffect : StatusEffect
    {
        private readonly int damagePerTurn;

        public DamageOverTimeEffect(int duration, int damagePerTurn, string effectName)
            : base(duration)
        {
            EffectName = effectName;
            this.damagePerTurn = damagePerTurn;
        }

        public override void OnTurnStart(Combatant target)
        {
            target.TakeDamage(damagePerTurn);
            Debug.Log($"{target.Data.displayName} recibe {damagePerTurn} de {EffectName}.");
        }
    }
}
