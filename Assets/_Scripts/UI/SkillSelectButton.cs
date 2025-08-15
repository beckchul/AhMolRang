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
    }

    public void OnClickButton()
    {
        OnClicked.Invoke(skillInfo);
    }
}
