using System;
using UnityEngine;

namespace NAPI.Data
{
    /// <summary>
    /// Condición configurable de una Ultimate.
    /// Es un contenedor de datos; la comprobación real se hará desde el sistema
    /// de evaluación de condiciones, no desde este archivo.
    /// </summary>
    [Serializable]
    public class UltimateCondition
    {
        [Tooltip("Tipo de condición que debe cumplirse.")]
        public UltimateConditionType conditionType;

        [Tooltip("Cómo se compara el valor actual con el valor configurado.")]
        public UltimateConditionOperator comparison = UltimateConditionOperator.GreaterThanOrEqual;

        [Header("Valor principal")]
        [Tooltip("Usado por condiciones numéricas. Porcentaje en 0-100 para HP/EN.")]
        [Min(0f)]
        public float value;

        [Header("Estado")]
        [Tooltip("Usado por HasStatusEffect / HasBuff / HasDebuff.")]
        public StatusEffectData statusEffect;

        [Header("Opciones adicionales")]
        [Tooltip("Reservado para condiciones booleanas o reglas futuras.")]
        public bool boolValue;
    }
}
