using UnityEngine;
using NAPI.Combat;

namespace NAPI.Data
{
    /// <summary>
    /// Base compartida por los buffs de stat (Ataque, Defensa, Velocidad,
    /// Regen. de Energía, Regen. de Vida, Vida Máxima). El único dato que
    /// varía entre ellos es a qué stat aplican; el porcentaje es lo que
    /// hace que sea "modificable por personaje específico": cada asset
    /// (ej. "Buff_Ataque_Markus" con 25%, "Buff_Ataque_Jack" con 15%)
    /// puede tener su propio valor sin tocar código.
    /// </summary>
    public abstract class StatModifierEffectData : StatusEffectData
    {
        [Header("Buff de estadística")]
        [Tooltip("Porcentaje del stat BASE del objetivo (post nivel/rango/rareza, pre-buffs). Ej: 0.2 = +20%")]
        [Range(0f, 2f)]
        public float percentage = 0.2f;
    }
}
