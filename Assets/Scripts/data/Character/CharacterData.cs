using UnityEngine;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "Character_", menuName = "NAPI/Character")]
    public class CharacterData : CombatantDataBase
    {
        [Header("Solo PJ")]
        [Tooltip("Nivel inicial con el que se crea/desbloquea este personaje")]
        public int startingLevel = 1;

        [Tooltip("Sprite/prefab con las partes por convención del GDD: cabeza, cabello, torso, brazo sup., antebrazo, mano, pierna sup., pantorrilla, pie, arma")]
        public GameObject characterPrefab;

        [Tooltip("Si puede extender guardia/concentración a todo el equipo (punto 5 del GDD)")]
        public bool canExtendGuardToTeam;
    }
}
