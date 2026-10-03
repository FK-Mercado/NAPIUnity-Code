using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NAPI.Combat;

namespace NAPI.UI
{
    public class CharacterCombatUI : MonoBehaviour
    {
        //-- UI References
        [Header("Combat")]

        [SerializeField] private BattleManager battleManager;

        [Tooltip("Posición del personaje dentro de BattleManager.PlayerTeam")]
        [SerializeField] private int characterIndex = 0;

        [Header("Character")]
        [SerializeField] private Image characterIcon;
        //--Stats
        [Header("HP")]
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private Slider hpBar;

        [Header("Energy")]
        [SerializeField] private TMP_Text energyText;
        [SerializeField] private Slider energyBar;

        [Header("Ultimate")]
        [SerializeField] private Slider ultimateBar;

        private Combatant combatant;

        private void Start()
        {
            if (battleManager == null)
                battleManager = FindAnyObjectByType<BattleManager>();

            InvokeRepeating(nameof(TryBindCombatant), 0f, 0.1f);
        }

        private void TryBindCombatant()
        {
            if (combatant != null)
                return;

            if (battleManager == null)
                return;

            if (battleManager.PlayerTeam == null)
                return;

            if (battleManager.PlayerTeam.Count <= characterIndex)
                return;

            combatant = battleManager.PlayerTeam[characterIndex];

            ConfigureBars();
            ConfigureCharacterVisual();

            CancelInvoke(nameof(TryBindCombatant));

            RefreshUI();
        }

        private void Update()
        {
            if (combatant == null)
                return;

            RefreshUI();
        }

        private void ConfigureBars()
        {
            if (hpBar != null)
            {
                hpBar.minValue = 0;
                hpBar.maxValue = combatant.MaxHP;
                hpBar.wholeNumbers = true;
            }

            if (energyBar != null)
            {
                energyBar.minValue = 0;
                energyBar.maxValue = combatant.MaxEnergy;
                energyBar.wholeNumbers = true;
            }

            if (ultimateBar != null)
            {
                ultimateBar.minValue = 0;
                ultimateBar.maxValue = 100;
                ultimateBar.wholeNumbers = true;
            }
        }

        private void ConfigureCharacterVisual()
        {
            if (characterIcon == null)
                return;

            if (combatant == null || combatant.Data == null)
            {
                characterIcon.sprite = null;
                return;
            }

            characterIcon.sprite = combatant.Data.icon;
        }
        private void RefreshUI()
        {
            if (hpBar != null)
                hpBar.value = combatant.CurrentHP;

            if (hpText != null)
                hpText.text = combatant.CurrentHP.ToString("N0");

            if (energyBar != null)
                energyBar.value = combatant.CurrentEnergy;

            if (energyText != null)
                energyText.text = combatant.CurrentEnergy.ToString("N0");

            if (ultimateBar != null)
                ultimateBar.value = combatant.UltimateCharge;
        }
    }
}