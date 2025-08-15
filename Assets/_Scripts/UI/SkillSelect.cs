using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static SkillDataScriptableObject;

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
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 각 버튼 세팅 작업
    /// </summary>
    public void SetButton()
    {
        // 스킬 리스트에서 가져온 후 처리
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
    /// 스킬 선택
    /// </summary>
    /// <param name="skillInfo"></param>
    public void OnSkillSelect(SkillDataBase skillInfo)
    {
        // 플레이어 정보에서 스킬 받아오면 처리 후 창 종료
        Debug.Log("OnSkillSelect");

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
            int k = Random.Range(0, n + 1);  // 0부터 N까지의 랜덤 값
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #endregion
}
