using UnityEngine;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "Enemy_", menuName = "NAPI/Enemy")]
    public class EnemyData : CombatantDataBase
    {
        [Header("Solo enemigos")]
        public AIBehaviour aiBehaviour;

        [Tooltip("Marca a este enemigo como jefe: en la misión ocupa la 4ta ronda con 2 oleadas (punto 2 del GDD)")]
        public bool isBoss;

        [Header("Recompensa")]
        public int currencyDrop;
        public ItemData[] possibleItemDrops;
    }
}
