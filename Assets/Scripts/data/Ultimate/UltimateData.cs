using System.Linq;
using UnityEngine;

namespace NAPI.Data
{
    /// <summary>
    /// Definición estática de una Ultimate.
    ///
    /// No mantiene la carga actual del combate.
    /// La carga actual seguirá perteneciendo al estado runtime de Combatant.
    /// </summary>
    [CreateAssetMenu(fileName = "Ultimate_", menuName = "NAPI/Ultimate")]
    public class UltimateData : ScriptableObject
    {
        [Header("Identidad")]
        [Tooltip("Identificador único de esta configuración de Ultimate.")]
        public string id;

        public string ultimateName;

        [TextArea]
        public string description;

        public Sprite icon;

        [Header("Generación de CAR")]
        [Tooltip("Reglas base de generación de carga. Los modificadores de pasivas/buffs se aplicarán en runtime.")]
        public UltimateChargeRule[] chargeRules;

        [Header("Puntos de lanzamiento")]
        [Tooltip("Puede haber uno o varios. El sistema tomará el mayor requiredCharge como carga máxima.")]
        public UltimateLaunchPoint[] launchPoints;

        /// <summary>
        /// Cantidad máxima de CAR necesaria para alcanzar el último punto de lanzamiento configurado.
        /// </summary>
        public int MaxCharge
        {
            get
            {
                if (launchPoints == null || launchPoints.Length == 0)
                    return 0;

                return launchPoints
                    .Where(point => point != null)
                    .Select(point => Mathf.Max(0, point.requiredCharge))
                    .DefaultIfEmpty(0)
                    .Max();
            }
        }

        /// <summary>
        /// Devuelve los puntos de lanzamiento que ya están disponibles según la CAR actual.
        /// La comprobación de condiciones adicionales se hará más adelante.
        /// </summary>
        public UltimateLaunchPoint[] GetChargeAvailableLaunchPoints(int currentCharge)
        {
            if (launchPoints == null)
                return System.Array.Empty<UltimateLaunchPoint>();

            return launchPoints
                .Where(point => point != null && currentCharge >= point.requiredCharge)
                .OrderBy(point => point.requiredCharge)
                .ToArray();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (launchPoints == null)
                return;

            // Mantiene los puntos ordenados por requerimiento para facilitar
            // edición y lectura en el Inspector.
            launchPoints = launchPoints
                .Where(point => point != null)
                .OrderBy(point => point.requiredCharge)
                .ToArray();

            // Evita valores inválidos básicos.
            foreach (var point in launchPoints)
            {
                point.requiredCharge = Mathf.Max(1, point.requiredCharge);
                point.customChargeCost = Mathf.Max(0, point.customChargeCost);
            }
        }
#endif
    }
}
