using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static SkillDataScriptableObject;

public class SkillSelect : MonoBehaviour
{
    /// <summary>
    /// Skill ���� ��ư
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
    /// �� ��ư ���� �۾�
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

            var activeSkillData = SkillManager.Instance.GetActiveSkillData(skillStatus.SkillId);

            if (activeSkillData != null)
            {
                activeSkillCount++;
            }

            var pasiveSkillData = SkillManager.Instance.GetPasiveSkillData(skillStatus.SkillId);

            if (pasiveSkillData != null)
            {
                pasiveSkillCount++;
            }
        }

        // ��ų ����Ʈ���� ������ �� ó��
        var skillData = SkillManager.Instance.skillData;
        List<SkillDataBase> skillList = new();

        if (skillData != null)
        {
            foreach (SkillDataBase data in skillData.activeSkills)
            {
                skillList.Add(data);
            }

            foreach (SkillDataBase data in skillData.passiveSkills)
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

            if (skill.skillType == SkillType.Active && SkillManager.Instance.MaxActiveSkillCount > activeSkillCount)
            {
                activeSkillCount++;
                skillSelectButtons[buttonIndex].SetButton(skill);
            }
            else if (skill.skillType == SkillType.Passive && SkillManager.Instance.MaxPasiveSkillCount > pasiveSkillCount)
            {
                pasiveSkillCount++;
                skillSelectButtons[buttonIndex].SetButton(skill);
            }

            buttonIndex++;
        }
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    /// <param name="skillInfo"></param>
    public void OnSkillSelect(SkillDataBase skillInfo)
    {
        SkillManager.Instance.SkillLevelUp(skillInfo.skillId);
        UIManager.Instance.UpdateSkillListUI();
        HideUI();
    }

    /// <summary>
    /// ���� ��ư
    /// </summary>
    public void OnClickReroll()
    {
        SetButton();
    }


    #region << ���� ���� >>
    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);  // 0���� N������ ���� ��
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #endregion
}
