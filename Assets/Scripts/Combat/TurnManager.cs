using System.Collections.Generic;
using UnityEngine;
using NAPI.Data;

namespace NAPI.Combat
{
    public class TurnManager : MonoBehaviour
    {
        private const float BASE_TURN_COST = 1000f;
        private const float SPEED_BASELINE = 50f;

        private float GetTurnDelay(Combatant combatant)
        {
            return BASE_TURN_COST / (combatant.FinalSpeed + SPEED_BASELINE);
        }

        private readonly List<Combatant> combatants = new();
        private BattleManager battleManager;

        private void Awake()
        {
            battleManager = GetComponent<BattleManager>();
        }

        private void Start()
        {
            if (battleManager == null)
            {
                Debug.LogError("TurnManager: BattleManager no encontrado.");
                return;
            }

            if (battleManager.EventBus == null)
            {
                Debug.LogError("TurnManager: EventBus es NULL.");
                return;
            }

            battleManager.EventBus.Subscribe<PlayerSkillSelectedEvent>(
                OnPlayerSkillSelected
            );

            Debug.Log("TurnManager: Suscrito a PlayerSkillSelectedEvent.");
        }

        public void Initialize(List<Combatant> players, List<Combatant> enemies)
        {
            combatants.Clear();
            combatants.AddRange(players);
            combatants.AddRange(enemies);

            foreach (Combatant combatant in combatants)
            {
                combatant.NextTurnValue = GetTurnDelay(combatant);
                Debug.Log($"{combatant.Data.name} -> Next Turn: {combatant.NextTurnValue}");
            }

            PrintNext10Turns();
            StartNextTurn();
        }

        private Combatant GetNextCombatant()
        {
            Combatant next = null;
            foreach (Combatant combatant in combatants)
            {
                if (!combatant.IsAlive) continue;
                if (next == null) { next = combatant; continue; }
                if (combatant.NextTurnValue < next.NextTurnValue) next = combatant;
            }
            return next;
        }

        private void PrintNext10Turns()
        {
            List<(Combatant combatant, float nextTurn)> simulated = new();

            foreach (Combatant combatant in combatants)
            {
                if (!combatant.IsAlive) continue;
                simulated.Add((combatant, combatant.NextTurnValue));
            }

            Debug.Log("=== Próximos 10 turnos ===");

            for (int i = 0; i < 10; i++)
            {
                int nextIndex = 0;
                for (int j = 1; j < simulated.Count; j++)
                {
                    if (simulated[j].nextTurn < simulated[nextIndex].nextTurn)
                        nextIndex = j;
                }

                var current = simulated[nextIndex];
                Debug.Log($"{i + 1}. {current.combatant.Data.displayName}");

                float delay = GetTurnDelay(current.combatant);
                simulated[nextIndex] = (current.combatant, current.nextTurn + delay);
            }
        }

        private void StartNextTurn()
        {
            Combatant current = GetNextCombatant();
            if (current == null) return;

            battleManager.EventBus?.Publish(new TurnStartEvent(current));

            current.ProcessTurnStartEffects();

            // Parálisis / interrupción: si algún efecto dice que este
            // combatiente no puede actuar, se salta la acción entera.
            if (current.ShouldSkipTurn())
            {
                Debug.Log($"{current.Data.displayName} no puede actuar este turno.");
                EndTurn(current);
                return;
            }

            Debug.Log($"Turno de {current.Data.name}");

            if (current.IsPlayerControlled)
                BeginPlayerTurn(current);
            else
                ExecuteEnemyTurn(current);
        }

        private void BeginPlayerTurn(Combatant combatant)
        {
            Debug.Log("=== BEGIN PLAYER TURN ===");

            Debug.Log($"Turno del jugador: {combatant.Data.displayName}");

            Debug.Log("Esperando acción del jugador...");

            battleManager.EventBus?.Publish(
                new PlayerActionRequestedEvent(combatant)
    );
        }

        private void ExecutePlayerTurn(Combatant attacker)
        {
            Combatant target = battleManager.GetFirstAliveEnemy();
            if (target == null) return;

            SkillExecutor.Execute(attacker, target, attacker.Data.basicAttack, battleManager.EventBus);
            EndTurn(attacker);
        }

        private void ExecuteEnemyTurn(Combatant attacker)
        {
            Combatant target = battleManager.GetFirstAlivePlayer();
            if (target == null) return;

            SkillExecutor.Execute(attacker, target, attacker.Data.basicAttack, battleManager.EventBus);
            EndTurn(attacker);
        }

        private void EndTurn(Combatant combatant)
        {
            combatant.ProcessTurnEndEffects();

            battleManager.EventBus?.Publish(new TurnEndEvent(combatant));

            if (battleManager.CheckBattleEnd())
                return;

            combatant.NextTurnValue += GetTurnDelay(combatant);
            Debug.Log($"{combatant.Data.displayName} -> Nuevo NextTurnValue: {combatant.NextTurnValue}");

            PrintNext10Turns();
            StartNextTurn();
        }

        private void OnPlayerSkillSelected(PlayerSkillSelectedEvent eventData)
        {
            if (eventData == null)
                return;

            Combatant attacker = eventData.Combatant;
            SkillData skill = eventData.Skill;

            if (attacker == null)
            {
                Debug.LogError(
                    "[TURN] No hay combatant para ejecutar la habilidad."
                );

                return;
            }

            if (skill == null)
            {
                Debug.LogError(
                    "[TURN] La habilidad seleccionada es NULL."
                );

                return;
            }

            Debug.Log(
                $"[TURN] Ejecutando {skill.skillName} con {attacker.Data.displayName}"
            );

            Combatant target = battleManager.GetFirstAliveEnemy();

            if (target == null)
            {
                Debug.LogWarning(
                    "[TURN] No hay enemigos vivos para atacar."
                );

                return;
            }

            SkillExecutor.Execute(
                attacker,
                target,
                skill,
                battleManager.EventBus
            );

            EndTurn(attacker);
        }
        private void OnDestroy()
        {
            if (battleManager == null || battleManager.EventBus == null)
                return;

            battleManager.EventBus.Unsubscribe<PlayerSkillSelectedEvent>(
                OnPlayerSkillSelected
            );
        }
    }
}
