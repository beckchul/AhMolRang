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

    public Action<SkillDataBase> OnClicked;

    private SkillDataBase skillInfo;

    /// <summary>
    /// 버튼 세팅
    /// </summary>
    public void SetButton(SkillDataBase SkillInfo)
    {
        skillInfo = SkillInfo;
        buttonIcon.sprite = skillInfo.skillIcon;
        buttonText.text = skillInfo.skillName;

        if (skillInfo.skillType == SkillType.Active)
        {
            activeIconBox.SetActive(true);
            pasiveIconBox.SetActive(false);
        }
        else
        {
            activeIconBox.SetActive(false);
            pasiveIconBox.SetActive(true);
        }

        if (skillInfo.skillLevel >= 5)
        {
            levelTextBox.SetActive(false);
            strengthenTextBox.SetActive(true);
        }
        else
        {
            levelTextBox.SetActive(true);
            strengthenTextBox.SetActive(false);

            levelText.text = "Lv." + skillInfo.skillLevel.ToString();
        }
    }

    public void OnClickButton()
    {
        OnClicked.Invoke(skillInfo);
    }
}
