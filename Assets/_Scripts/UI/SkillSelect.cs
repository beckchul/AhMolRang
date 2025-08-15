using UnityEngine;

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
        foreach (SkillSelectButton button in skillSelectButtons)
        {
            button.SetButton(1, 1, 1);
        }
    }

    /// <summary>
    /// ��ư Ŭ�� �� ó��
    /// </summary>
    /// <param name="skillType"></param>
    /// <param name="skillId"></param>
    /// <param name="skillLevel"></param>
    public void OnSkillSelect(int skillType, int skillId, int skillLevel)
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
}
