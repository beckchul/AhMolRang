using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    /// <summary>
    /// ��ų ���� â
    /// </summary>
    [SerializeField]
    private SkillSelect skillSelectUI;

    /// <summary>
    /// ���� ��ų ������ ������
    /// </summary>
    [SerializeField]
    private HasSkillList hasSkillListUI;

    /// <summary>
    /// �Ͻ� ���� â
    /// </summary>
    [SerializeField]
    private GameObject pause;

    /// <summary>
    /// �Ͻ� ���� üũ��
    /// </summary>
    [HideInInspector]
    public bool IsPaused = false;

    private void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateSkillListUI();
        ShowSkillSelectUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowSkillSelectUI()
    {
        skillSelectUI.ShowUI();
    }

    public void UpdateSkillListUI()
    {
        hasSkillListUI.UpdateHasSkillList();
    }

    /// <summary>
    /// �Ͻ� ���� ��ư
    /// </summary>
    public void OnClickPauseButton()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        pause.SetActive(true);
    }

    /// <summary>
    /// ���� �簳 ��ư
    /// </summary>
    public void OnClickPauseEndButton()
    {
        Time.timeScale = 1f;
        IsPaused = true;
        pause.SetActive(false);
    }
}
