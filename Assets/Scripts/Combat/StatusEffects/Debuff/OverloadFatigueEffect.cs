using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    /// <summary>
    /// Sin Data/[CreateAssetMenu] propio: solo lo crea OverloadEffect al
    /// expirar, aplicando un debuff en las mismas stats que estuvieron
    /// buffeadas (y solo esas — respeta el mismo OverloadStat).
    /// </summary>
    public class OverloadFatigueEffect : StatusEffect
    {
        private readonly OverloadStat enabledStats;
        private readonly StatAmounts amounts;

        public OverloadFatigueEffect(int duration, OverloadStat enabledStats, StatAmounts amounts) : base(duration)
        {
            EffectName = "Fatiga";
            this.enabledStats = enabledStats;
            this.amounts = amounts;
        }

        public override void OnApply(Combatant target)
        {
            if (Has(OverloadStat.Attack)) target.ModifyAttack(-amounts.Attack);
            if (Has(OverloadStat.Defense)) target.ModifyDefense(-amounts.Defense);
            if (Has(OverloadStat.Speed)) target.ModifySpeed(-amounts.Speed);
            if (Has(OverloadStat.MaxHP)) target.DecreaseMaxHP(amounts.MaxHP);

            Debug.Log($"{target.Data.displayName} sufre Fatiga ({RemainingTurns} turnos).");
        }

        public override void OnRemove(Combatant target)
        {
            if (Has(OverloadStat.Attack)) target.ModifyAttack(amounts.Attack);
            if (Has(OverloadStat.Defense)) target.ModifyDefense(amounts.Defense);
            if (Has(OverloadStat.Speed)) target.ModifySpeed(amounts.Speed);
            if (Has(OverloadStat.MaxHP)) target.IncreaseMaxHP(amounts.MaxHP);

            Debug.Log($"{target.Data.displayName} se recupera de la Fatiga.");
        }

        private bool Has(OverloadStat flag) => (enabledStats & flag) != 0;
    }
}
