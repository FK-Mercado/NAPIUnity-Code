using System;

namespace NAPI.Combat
{
    /// <summary>
    /// Estado runtime de una Ultimate durante una batalla.
    ///
    /// No genera CAR.
    /// No evalúa condiciones.
    /// No ejecuta habilidades.
    ///
    /// La configuración estática pertenece a UltimateData.
    /// La carga actual pertenece a Combatant.CurrentCharge.
    /// Este objeto solamente conecta ambas cosas con el Combatant
    /// que posee la Ultimate durante la batalla.
    /// </summary>
    public class UltimateRuntime
    {
        /// <summary>
        /// Combatant que posee esta Ultimate durante la batalla.
        /// </summary>
        public Combatant Owner { get; }

        /// <summary>
        /// Configuración estática de la Ultimate.
        /// </summary>
        public NAPI.Data.UltimateData Data { get; }

        /// <summary>
        /// CAR actual del propietario.
        ///
        /// No se almacena una segunda copia de la carga aquí.
        /// La fuente de verdad continúa siendo Combatant.CurrentCharge.
        /// </summary>
        public int CurrentCharge => Owner.CurrentCharge;

        /// <summary>
        /// Máxima CAR definida por los LaunchPoints de la Ultimate.
        /// </summary>
        public int MaxCharge => Data.MaxCharge;

        public UltimateRuntime(
            Combatant owner,
            NAPI.Data.UltimateData data)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Devuelve los puntos de lanzamiento que cumplen el requisito
        /// de CAR actualmente.
        ///
        /// Las condiciones adicionales todavía NO se evalúan aquí.
        /// </summary>
        public NAPI.Data.UltimateLaunchPoint[] GetChargeAvailableLaunchPoints()
        {
            return Data.GetChargeAvailableLaunchPoints(CurrentCharge);
        }
    }
}