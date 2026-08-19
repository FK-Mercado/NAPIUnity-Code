using System;
using UnityEngine;

namespace NAPI.Data
{
    /// <summary>
    /// Un punto de lanzamiento de una Ultimate.
    /// Una Ultimate puede tener uno o varios puntos de lanzamiento.
    /// Ejemplo: 100 CAR = versión normal, 200 CAR = versión potenciada.
    /// </summary>
    [Serializable]
    public class UltimateLaunchPoint
    {
        [Header("Identidad")]
        [Tooltip("Nombre mostrado para identificar este nivel de lanzamiento en el Inspector/UI.")]
        public string displayName = "Lanzamiento 1";

        [Tooltip("Cantidad mínima de CAR necesaria para desbloquear este lanzamiento.")]
        [Min(1)]
        public int requiredCharge = 100;

        [Header("Ejecución")]
        [Tooltip("SkillData que se ejecutará cuando se elija este punto de lanzamiento.")]
        public SkillData skill;

        [Header("Condiciones adicionales")]
        [Tooltip("Todas las condiciones configuradas deben cumplirse.")]
        public UltimateCondition[] conditions;

        [Header("Consumo de CAR")]
        public UltimateChargeConsumptionMode chargeConsumption = UltimateChargeConsumptionMode.ResetToZero;

        [Tooltip("Solo se utiliza cuando Charge Consumption = Spend Custom Amount.")]
        [Min(0)]
        public int customChargeCost = 0;
    }
}
