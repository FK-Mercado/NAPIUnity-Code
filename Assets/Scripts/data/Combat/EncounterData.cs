using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    [CreateAssetMenu(
        fileName = "Encounter",
        menuName = "NAPI/Combat/Encounter")]
    public class EncounterData : ScriptableObject
    {
        public EnemyData[] enemies;
    }
}