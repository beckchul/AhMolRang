using System;
using UnityEngine;

public class SkillSelectButton : MonoBehaviour
{
    public Action<int, int, int> OnClicked;

    public int SkillType;

    public int SkillId;

    public int SkillLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetButton(int skillType, int skillId, int skillLevel)
    {
        SkillType = skillType;
        SkillId = skillId;
        SkillLevel = skillLevel;
    }

    public void OnClickButton()
    {
        OnClicked.Invoke(SkillType, SkillId, SkillLevel);
    }
}
