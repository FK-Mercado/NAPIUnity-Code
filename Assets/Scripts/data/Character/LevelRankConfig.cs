using UnityEngine;

namespace NAPI.Data
{
    /// <summary>
    /// Configuración global del sistema de progresión. Se crea UN solo
    /// asset en todo el proyecto (no uno por personaje) y todos los
    /// CharacterProgression lo referencian.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelRankConfig", menuName = "NAPI/Level Rank Config")]
    public class LevelRankConfig : ScriptableObject
    {
        [Header("Rangos (límites de nivel de 20 en 20)")]
        [Tooltip("Nivel 1-20 sin romper nada; para pasar de 20 hay que romper el rango 0, etc.")]
        public int[] rankLevelCaps = { 20, 40, 60, 80, 100 };

        [System.Serializable]
        public class RankBreakCost
        {
            public int currencyCost;
            public ItemData requiredResource;
            public int resourceAmount;
        }

        [Tooltip("Costo para romper cada límite. Índice 0 = costo para poder pasar del nivel 20, índice 1 = del 40, etc. Debe tener un elemento menos que rankLevelCaps (el último tope, 100, no se rompe).")]
        public RankBreakCost[] rankBreakCosts;

        [Header("Curva de experiencia")]
        [Tooltip("XP necesaria = baseXP * nivel ^ exponente")]
        public float baseXPToLevelUp = 100f;
        public float xpCurveExponent = 1.4f;

        public int GetXPRequired(int currentLevel)
        {
            return Mathf.RoundToInt(baseXPToLevelUp * Mathf.Pow(currentLevel, xpCurveExponent));
        }

        public float GetRarityMultiplier(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Comun: return 1.0f;
                case Rarity.Raro: return 1.15f;
                case Rarity.Epico: return 1.3f;
                case Rarity.Legendario: return 1.5f;
                default: return 1.0f;
            }
        }
    }
}
