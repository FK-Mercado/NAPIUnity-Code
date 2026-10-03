using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NAPI.Data;

namespace NAPI.UI
{
    public class SkillButtonUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button button;
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text skillNameText;

        private SkillData skill;
        private UnityAction<SkillData> onSkillSelected;

        private void Awake()
        {
            if (button == null)
                button = GetComponent<Button>();
        }

        public void Setup(SkillData skillData, UnityAction<SkillData> onSelected)
        {
            skill = skillData;
            onSkillSelected = onSelected;

            if (skill == null)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);

            if (skillNameText != null)
                skillNameText.text = skill.skillName;

            if (iconImage != null)
                iconImage.sprite = skill.icon;
                iconImage.enabled = skill.icon != null;

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnClicked);
            }
        }

        private void OnClicked()
        {
            if (skill == null)
                return;

            Debug.Log($"Skill seleccionada: {skill.skillName}");

            onSkillSelected?.Invoke(skill);
        }
    }
}