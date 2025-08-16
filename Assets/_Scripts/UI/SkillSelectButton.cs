using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillDataScriptableObject;

public class SkillSelectButton : MonoBehaviour
{
    [SerializeField]
    private Image buttonIcon;

    [SerializeField]
    private TMP_Text buttonText;

    [SerializeField]
    private GameObject activeIconBox;

    [SerializeField]
    private GameObject pasiveIconBox;

    [SerializeField]
    private GameObject levelTextBox;

    [SerializeField]
    private GameObject strengthenTextBox;

    [SerializeField]
    private TMP_Text skillLabelText;

    [SerializeField]
    private TMP_Text levelText;

    public Action<int> OnClicked;

    private int skillId;

    /// <summary>
    /// 버튼 세팅
    /// </summary>
    public void SetButton(SkillStatus skillStatus)
    {
        skillId = skillStatus.SkillId;

        var skillData = SkillManager.Instance.GetSkillData(skillStatus.SkillId);

        buttonIcon.sprite = skillData.skillIcon;
        buttonText.text = skillData.skillName;

        if (skillData.skillType == SkillType.Active)
        {
            activeIconBox.SetActive(true);
            pasiveIconBox.SetActive(false);
        }
        else
        {
            activeIconBox.SetActive(false);
            pasiveIconBox.SetActive(true);
        }

        if (skillData.skillLevel >= 5)
        {
            levelTextBox.SetActive(false);
            strengthenTextBox.SetActive(true);
        }
        else
        {
            levelTextBox.SetActive(true);
            strengthenTextBox.SetActive(false);

            levelText.text = "Lv." + (skillStatus.Level + 1).ToString();
        }
    }

    public void OnClickButton()
    {
        OnClicked.Invoke(skillId);
    }
}
