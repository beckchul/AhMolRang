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
        foreach (SkillSelectButton button in skillSelectButtons)
        {
            button.SetButton(1, 1, 1);
        }
    }

    /// <summary>
    /// 버튼 클릭 시 처리
    /// </summary>
    /// <param name="skillType"></param>
    /// <param name="skillId"></param>
    /// <param name="skillLevel"></param>
    public void OnSkillSelect(int skillType, int skillId, int skillLevel)
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
}
