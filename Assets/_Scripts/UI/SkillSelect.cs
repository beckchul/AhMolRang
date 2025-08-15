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
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �� ��ư ���� �۾�
    /// </summary>
    public void SetButton()
    {
        // ��ų ����Ʈ���� ������ �� ó��
        var skillData = SkillManager.Instance.skillData;
        var playerSkills = SkillManager.Instance.Skills;
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

        foreach (var (button, index) in skillSelectButtons.Select((value, idx) => (value, idx)))
        {
            button.SetButton(skillList[index]);
        }
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    /// <param name="skillInfo"></param>
    public void OnSkillSelect(SkillDataBase skillInfo)
    {
        // �÷��̾� �������� ��ų �޾ƿ��� ó�� �� â ����
        Debug.Log("OnSkillSelect");

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
