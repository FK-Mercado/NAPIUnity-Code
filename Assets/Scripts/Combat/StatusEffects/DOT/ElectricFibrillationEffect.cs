using UnityEngine;

namespace NAPI.Combat
{
    /// <summary>
    /// "Daño en cada acción que realiza" se aproxima como daño en
    /// OnTurnStart, ya que hoy cada combatiente hace una sola acción por
    /// turno. Si más adelante un personaje puede actuar más de una vez
    /// por turno, este es el lugar a ajustar.
    /// </summary>
    public class ElectricFibrillationEffect : StatusEffect
    {
        private readonly int damagePerTurn;
        private readonly float interruptChance;

        public ElectricFibrillationEffect(int duration, int damagePerTurn, float interruptChance, string effectName)
            : base(duration)
        {
            EffectName = effectName;
            this.damagePerTurn = damagePerTurn;
            this.interruptChance = interruptChance;
        }

        public override void OnTurnStart(Combatant target)
        {
            target.TakeDamage(damagePerTurn);
            Debug.Log($"{target.Data.displayName} recibe {damagePerTurn} de {EffectName}.");
        }

        public override bool ShouldSkipHolderTurn(Combatant holder)
        {
            if (Random.value < interruptChance)
            {
                Debug.Log($"{holder.Data.displayName} es interrumpido por {EffectName}.");
                return true;
            }
            return false;
        }
    }
}
