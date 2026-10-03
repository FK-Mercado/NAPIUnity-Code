using System.Collections.Generic;
using UnityEngine;
using NAPI.Combat;
using NAPI.Data;

namespace NAPI.UI
{
    public class SkillSelectorUI : MonoBehaviour
    {
        [Header("Battle")]
        [SerializeField] private BattleManager battleManager;

        [Header("Skill Buttons")]
        [SerializeField] private List<SkillButtonUI> skillButtons = new();

        private Combatant combatant;

        private void Start()
        {
            Debug.Log("=== SKILL SELECTOR UI START ===");

            if (battleManager == null)
                battleManager = FindAnyObjectByType<BattleManager>();

            if (battleManager == null)
            {
                Debug.LogError("SkillSelectorUI: No se encontró BattleManager.");
                return;
            }

            if (battleManager.EventBus == null)
            {
                Debug.LogError("SkillSelectorUI: EventBus es NULL.");
                return;
            }

            Debug.Log("SkillSelectorUI: Suscribiéndose al EventBus.");

            battleManager.EventBus.Subscribe<PlayerActionRequestedEvent>(
                OnPlayerActionRequested
            );

            Debug.Log("SkillSelectorUI: Suscripción realizada.");
        }

        private void OnDestroy()
        {
            if (battleManager == null || battleManager.EventBus == null)
                return;

            battleManager.EventBus.Unsubscribe<PlayerActionRequestedEvent>(
                OnPlayerActionRequested
            );
        }

        private void OnPlayerActionRequested(
            PlayerActionRequestedEvent eventData)
        {
            combatant = eventData.Combatant;

            Debug.Log(
                $"[UI] Turno recibido: {combatant.Data.displayName}"
            );

            ConfigureSkills();
            Debug.Log($"[UI] Habilidades de {combatant.Data.displayName}:");
        }

        private void ConfigureSkills()
        {
            if (combatant == null)
                return;

            List<SkillData> availableSkills = new();

            // Ataque básico
            if (combatant.Data.basicAttack != null)
                availableSkills.Add(combatant.Data.basicAttack);

            // Skills normales
            if (combatant.Data.skills != null)
            {
                foreach (SkillData skill in combatant.Data.skills)
                {
                    if (skill != null)
                        availableSkills.Add(skill);
                }
                foreach (SkillData skill in availableSkills)
                {
                    Debug.Log($"[UI] Skill disponible: {skill.skillName}");
                }
            }

            // Ultimate
            if (combatant.Data.ultimate != null)
                availableSkills.Add(combatant.Data.ultimate);

            for (int i = 0; i < skillButtons.Count; i++)
            {
                if (i < availableSkills.Count)
                {
                    skillButtons[i].gameObject.SetActive(true);
                    skillButtons[i].Setup(availableSkills[i], OnSkillSelected);
                }
                else
                {
                    skillButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnSkillSelected(SkillData selectedSkill)
        {
            if (combatant == null)
            {
                Debug.LogError(
                    "[UI] No hay combatant activo para seleccionar habilidad."
                );
                return;
            }

            if (selectedSkill == null)
            {
                Debug.LogError(
                    "[UI] Se intentó seleccionar una habilidad NULL."
                );
                return;
            }

            Debug.Log($"[UI] Habilidad seleccionada: {selectedSkill.skillName}");

            battleManager.EventBus.Publish(new PlayerSkillSelectedEvent(combatant, selectedSkill));
        }
    }
}