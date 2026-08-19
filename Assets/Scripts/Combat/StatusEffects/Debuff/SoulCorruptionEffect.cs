using UnityEngine;

namespace NAPI.Combat
{
    /// <summary>
    /// Nota de diseño: a propósito NO revierte el drenaje al expirar
    /// (a diferencia de MaxHPDebuffEffect). "Corrupción" implica un daño
    /// permanente a la vida máxima, no un debuff temporal — solo curación
    /// dedicada (o un ítem específico) debería recuperar ese HP máximo.
    /// </summary>
    public class SoulCorruptionEffect : StatusEffect
    {
        private readonly int amountPerTurn;

        public SoulCorruptionEffect(int duration, int amountPerTurn) : base(duration)
        {
            EffectName = "Corrupción del Alma";
            this.amountPerTurn = amountPerTurn;
        }

        public override void OnTurnStart(Combatant target)
        {
            target.DecreaseMaxHP(amountPerTurn);
            Debug.Log($"{target.Data.displayName} pierde {amountPerTurn} de vida máxima por Corrupción del Alma.");
        }
    }
}
