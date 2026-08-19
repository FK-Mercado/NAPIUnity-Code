using System;
using UnityEngine;

namespace NAPI.Data
{
    /// <summary>
    /// Regla base de generación de CAR.
    /// La ejecución de estos eventos se conectará posteriormente al sistema de combate.
    /// </summary>
    [Serializable]
    public class UltimateChargeRule
    {
        public UltimateChargeEventType eventType;

        [Tooltip("Cantidad base de CAR que genera este evento.")]
        [Min(0)]
        public int baseAmount = 0;

        [Tooltip("Si está desactivado, esta regla no generará CAR aunque permanezca configurada.")]
        public bool enabled = true;
    }
}
