using UnityEngine;

namespace NAPI.Combat
{
    public class RadiantJudgmentEffect : StatusEffect
    {
        private readonly int damagePerTurn;
        private readonly float evasionReduction;

        public RadiantJudgmentEffect(int duration, int damagePerTurn, float evasionReduction, string effectName)
            : base(duration)
        {
            EffectName = effectName;
            this.damagePerTurn = damagePerTurn;
            this.evasionReduction = evasionReduction;
        }

        public override void OnApply(Combatant target)
        {
            target.ModifyEvasion(-evasionReduction);
            Debug.Log($"{target.Data.displayName} queda expuesto por el brillo de {EffectName}.");
        }

        public override void OnTurnStart(Combatant target)
        {
            target.TakeDamage(damagePerTurn);
            Debug.Log($"{target.Data.displayName} recibe {damagePerTurn} de {EffectName}.");
        }

        public override void OnRemove(Combatant target)
        {
            target.ModifyEvasion(evasionReduction);
        }
    }
}
