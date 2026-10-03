using UnityEngine;

namespace NAPI.Data
{
    public abstract class CombatantDataBase : ScriptableObject
    {
        //-- Identidad, visual y descripción del combatiente
        [Header("Identidad")]
        public string id;
        public string displayName;
        public Sprite icon;

        //-- Stats base
        [Header("Stats base (max; HP, EN, CAR); def, ATK, SPD")]
        public int maxHP = 100;
        public int maxEnergy = 100;
        public int attack = 10;
        public int defense = 10;
        public int speed = 10;

        //-- Afinidad elemental, habilidades y progresión
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
