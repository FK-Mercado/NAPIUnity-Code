using UnityEngine;
namespace NAPI.Data

{
    [CreateAssetMenu(fileName = "Skill_", menuName = "NAPI/Skill")]
    public class SkillData : ScriptableObject
    {
        [Header("Identidad")]
        [Tooltip("Tiene que ser único: se usa como clave para guardar el nivel de ESTA skill por personaje.")]
        public string id;
        public string skillName;
        [TextArea] public string description;

        [Header("Clasificación (punto 4 del GDD)")]
        public SkillType skillType;
        public ElementType element;
        public TargetType targetType;

        [Header("Alcance")]
        public AttackRangeType attackRange = AttackRangeType.Melee;

        [Header("Costo")]
        [Tooltip("Consumo de energía (EN). El ataque básico suele ser 0.")]
        public int energyCost;

        [Header("Daño (porcentual al Ataque del que la usa)")]
        [Tooltip("1 = 100% del Ataque. Ej: 1.4 = 140% del ATK del atacante.")]
        public float minDamagePercentage = 1.0f;
        public float maxDamagePercentage = 1.2f;
        public int numberOfHits = 1;
        public float turnCostMultiplier = 1f;

        [Header("Nivel de la skill")]
        [Tooltip("Cuánto sube minDamagePercentage y maxDamagePercentage por CADA nivel de la skill por encima de 1.")]
        public float damagePercentagePerLevel = 0.05f;
        public int maxSkillLevel = 10;
        [Tooltip("Costo de moneda para subir de nivel N a N+1 = currencyCostPerLevel * N")]
        public int currencyCostPerLevel = 100;
        public ItemData levelUpResource;
        public int resourceCostPerLevel = 1;

        [Header("Definitivo (punto 4 y 7 del GDD)")]
        [Tooltip("Sólo aplica a skillType = Definitivo. Requiere EN >= 50% y carga (CAR) al 100%.")]
        public bool requiresUltimateCharge;

        [Header("Efectos sobre el objetivo")]
        public StatusEffectData[] appliedStatusEffects;

        [Header("Efectos sobre quien usa la skill")]
        public StatusEffectData[] selfAppliedStatusEffects;
    }
}
