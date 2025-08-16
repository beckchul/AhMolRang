using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    /// <summary>
    /// Skill 선택 버튼
    /// </summary>
    [SerializeField]
    private SkillSelectButton[] skillSelectButtons;

    private void Awake()
    {
        foreach (SkillSelectButton button in skillSelectButtons)
        {
            button.OnClicked += OnSkillSelect;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnDestroy()
    {
        foreach (SkillSelectButton button in skillSelectButtons)
        {
            button.OnClicked -= OnSkillSelect;
        }
    }
    public void ShowUI()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 각 버튼 세팅 작업
    /// </summary>
    public void SetButton()
    {
        int activeSkillCount = 0;
        int pasiveSkillCount = 0;
        var playerSkills = SkillManager.Instance.Skills;

        foreach (var skill in playerSkills)
        {
            var skillStatus = skill.Value;

            if (skillStatus.Level == 0)
            {
                continue;
            }

            var activeSkillData = SkillManager.Instance.GetActiveSkillStatus(skillStatus.SkillId);

            if (activeSkillData != null)
            {
                activeSkillCount++;
            }

            var pasiveSkillData = SkillManager.Instance.GetPasiveSkillStatus(skillStatus.SkillId);

            if (pasiveSkillData != null)
            {
                pasiveSkillCount++;
            }
        }

        // 스킬 리스트에서 가져온 후 처리
        var skillData = SkillManager.Instance.Skills;
        List<SkillStatus> skillList = new();

        if (skillData != null)
        {
            foreach (SkillStatus data in skillData.Values)
            {
                skillList.Add(data);
            }
        }

        ShuffleList(skillList);

        int buttonIndex = 0;

        foreach (var (skill, index) in skillList.Select((value, idx) => (value, idx)))
        {
            if (buttonIndex >= skillSelectButtons.Length)
            {
                break;
            }

            var curSkillData = SkillManager.Instance.GetSkillData(skill.SkillId);

            if (curSkillData.skillType == SkillType.Active)
            {
                if (HasSkillCheck(skill.SkillId) || SkillManager.Instance.MaxActiveSkillCount > activeSkillCount)
                {
                    skillSelectButtons[buttonIndex].SetButton(skill);
                    buttonIndex++;
                }
            }
            else if (curSkillData.skillType == SkillType.Passive)
            {
                if (HasSkillCheck(skill.SkillId) || SkillManager.Instance.MaxPasiveSkillCount > pasiveSkillCount)
                {
                    skillSelectButtons[buttonIndex].SetButton(skill);
                    buttonIndex++;
                }
            }
        }
    }

    private bool HasSkillCheck(int skillId)
    {
        var playerSkills = SkillManager.Instance.Skills;

        foreach (var playerSkill in playerSkills)
        {
            var skillStatus = playerSkill.Value;

            if (skillStatus.Level == 0)
            {
                continue;
            }

            if (skillStatus.SkillId == skillId)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 스킬 선택
    /// </summary>
    /// <param name="skillInfo"></param>
    public void OnSkillSelect(int skillId)
    {
        SkillManager.Instance.SkillLevelUp(skillId);
        UIManager.Instance.UpdateSkillListUI();
        HideUI();
    }

    /// <summary>
    /// 리롤 버튼
    /// </summary>
    public void OnClickReroll()
    {
        SetButton();
    }


    #region << 랜덤 섞기 >>
    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #endregion
}
