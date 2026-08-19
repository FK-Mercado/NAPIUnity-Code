using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    /// <summary>Cuánto sube/baja cada una de las 4 stats. Un campo en 0
    /// significa "esa stat no está marcada en este asset" — no hace falta
    /// que OverloadEffect sepa nada de flags, solo suma lo que le pasaron.</summary>
    public readonly struct StatAmounts
    {
        public readonly int Attack;
        public readonly int Defense;
        public readonly int Speed;
        public readonly int MaxHP;

        public StatAmounts(int attack, int defense, int speed, int maxHP)
        {
            Attack = attack;
            Defense = defense;
            Speed = speed;
            MaxHP = maxHP;
        }
    }

    public class OverloadEffect : StatusEffect
    {
        private readonly OverloadStat enabledStats;
        private readonly StatAmounts boost;
        private readonly int fatigueDuration;
        private readonly StatAmounts fatigue;

        public OverloadEffect(int duration, OverloadStat enabledStats, StatAmounts boost, int fatigueDuration, StatAmounts fatigue)
            : base(duration)
        {
            EffectName = "Sobrecarga";
            this.enabledStats = enabledStats;
            this.boost = boost;
            this.fatigueDuration = fatigueDuration;
            this.fatigue = fatigue;
        }

        public override void OnApply(Combatant target)
        {
            if (Has(OverloadStat.Attack)) target.ModifyAttack(boost.Attack);
            if (Has(OverloadStat.Defense)) target.ModifyDefense(boost.Defense);
            if (Has(OverloadStat.Speed)) target.ModifySpeed(boost.Speed);
            if (Has(OverloadStat.MaxHP)) target.IncreaseMaxHP(boost.MaxHP);

            Debug.Log($"{target.Data.displayName} se sobrecarga ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            if (Has(OverloadStat.Attack)) target.ModifyAttack(-boost.Attack);
            if (Has(OverloadStat.Defense)) target.ModifyDefense(-boost.Defense);
            if (Has(OverloadStat.Speed)) target.ModifySpeed(-boost.Speed);
            if (Has(OverloadStat.MaxHP)) target.DecreaseMaxHP(boost.MaxHP);

            Debug.Log($"{target.Data.displayName} termina la Sobrecarga y sufre Fatiga.");
            target.AddStatusEffect(new OverloadFatigueEffect(fatigueDuration, enabledStats, fatigue));
        }

        private bool Has(OverloadStat flag) => (enabledStats & flag) != 0;
    }
}
