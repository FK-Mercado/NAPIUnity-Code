using System.Collections.Generic;
using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private CharacterData playerData;
        [SerializeField] private EnemyData[] enemies;

        [Header("Progresión")]
        [Tooltip("El único asset de configuración de rangos/XP del proyecto")]
        [SerializeField] private LevelRankConfig progressionConfig;

        [Header("Progresión - valores de prueba (simulan un save cargado)")]
        [SerializeField] private int testLevel = 1;
        [SerializeField] private int testCurrentXP = 0;
        [SerializeField] private int testUnlockedRankIndex = 0;

        private readonly List<Combatant> playerTeam = new();
        private readonly List<Combatant> enemyTeam = new();

        public IReadOnlyList<Combatant> PlayerTeam => playerTeam;
        public IReadOnlyList<Combatant> EnemyTeam => enemyTeam;

        private TurnManager turnManager;

        private void Start()
        {
            turnManager = GetComponent<TurnManager>();

            CreatePlayerTeam();
            CreateEnemyTeam();

            StartBattle();
        }

        private void StartBattle()
        {
            turnManager.Initialize(
                playerTeam,
                enemyTeam
            );
        }

        private void CreatePlayerTeam()
        {
            playerTeam.Clear();

            // 1) La progresión se arma con el asset de datos del PJ + el
            //    config global de rangos/XP. SetState simula acá lo que en
            //    el juego real vendría del archivo de guardado.
            CharacterProgression progression =
                new CharacterProgression(playerData, progressionConfig);

            progression.SetState(testLevel, testCurrentXP, testUnlockedRankIndex);

            // 2) Se pasa la progresión al Combatant: adentro, el constructor
            //    llama a progression.GetMaxHP()/GetAttack()/etc en vez de
            //    leer los stats crudos de playerData.
            Combatant player =
                new Combatant(
                    playerData,
                    true,
                    progression
                );

            LogResultingStats(playerData.displayName, progression, player);

            playerTeam.Add(player);
        }

        private void LogResultingStats(string name, CharacterProgression progression, Combatant combatant)
        {
            Debug.Log(
                $"{name} | Nivel {progression.Level} (tope actual: {progression.CurrentLevelCap}) | " +
                $"Rareza: {playerData.rarity} | " +
                $"HP:{combatant.MaxHP} EN:{combatant.MaxEnergy} " +
                $"ATK:{combatant.Attack} DEF:{combatant.Defense} SPD:{combatant.Speed}"
            );
        }

        private void CreateEnemyTeam()
        {
            enemyTeam.Clear();
            foreach (EnemyData enemyData in enemies)
            {
                // Los enemigos NO reciben CharacterProgression: sus stats
                // son siempre los fijos de EnemyData (progression = null,
                // el default del parámetro en Combatant).
                Combatant enemy =
                    new Combatant(
                        enemyData,
                        false
                    );
                enemyTeam.Add(enemy);
            }
        }

        public Combatant GetFirstAliveEnemy()
        {
            foreach (Combatant enemy in enemyTeam)
            {
                if (enemy.IsAlive)
                    return enemy;
            }

            return null;
        }

        public Combatant GetFirstAlivePlayer()
        {
            foreach (Combatant player in playerTeam)
            {
                if (player.IsAlive)
                    return player;
            }

            return null;
        }


        public bool CheckBattleEnd()
        {
            bool enemiesDefeated =
                enemyTeam.TrueForAll(e => !e.IsAlive);

            if (enemiesDefeated)
            {
                Debug.Log("VICTORIA");
                return true;
            }

            bool playersDefeated =
                playerTeam.TrueForAll(p => !p.IsAlive);

            if (playersDefeated)
            {
                Debug.Log("DERROTA");
                return true;
            }
            return false;
        }

    }

}
