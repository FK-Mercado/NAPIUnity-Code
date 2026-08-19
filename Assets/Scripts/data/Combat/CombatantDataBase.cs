using UnityEngine;

namespace NAPI.Data
{
    /// <summary>
    /// Base compartida por CharacterData y EnemyData.
    /// Contiene únicamente lo que el sistema de combate (Combatant,
    /// TurnManager, DamageCalculator) necesita leer sin importarle
    /// si el combatiente es un PJ o un enemigo.
    /// </summary>
    public abstract class CombatantDataBase : ScriptableObject
    {
        [Header("Identidad")]
        public string id;
        public string displayName;
        public Sprite icon;

        [Header("Stats base (punto 7 del GDD: HP, EN, CAR)")]
        public int maxHP = 100;
        public int maxEnergy = 100;
        public int attack = 10;
        public int defense = 10;
        public int speed = 10;

        [Header("Afinidad elemental")]
        public ElementType affinity;
        [Tooltip("Elemento al que este combatiente es débil (activa Break, punto 3 del GDD)")]
        public ElementType weakness;

        [Header("Habilidades")]
        public SkillData basicAttack;      // ataque básico, todos lo tienen
        public SkillData[] skills;         // habilidades básicas + avanzadas
        public SkillData ultimate;         // movimiento definitivo

        [Header("Progresión (nivel 1-100, ver LevelRankConfig)")]
        [Tooltip("Grado/rareza: multiplica el resultado final de cada stat")]
        public Rarity rarity = Rarity.Comun;
        [Tooltip("Cuánto sube cada stat por nivel por encima del valor base (nivel 1 = solo el stat base)")]
        public float hpGrowthPerLevel = 8f;
        public float energyGrowthPerLevel = 2f;
        public float attackGrowthPerLevel = 2f;
        public float defenseGrowthPerLevel = 1.5f;
        public float speedGrowthPerLevel = 0.5f;
    }
}
