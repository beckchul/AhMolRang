using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    public TMP_Text waveText;

    /// <summary>
    /// �÷��̾� ü�¹�
    /// </summary>
    [SerializeField]
    private SliderBar hpBarUI;

    /// <summary>
    /// ���̺� Ÿ�̸�
    /// </summary>
    [SerializeField]
    private WaveTimer waveTimerUI;

    /// <summary>
    /// ����ġ ��
    /// </summary>
    [SerializeField]
    private SliderBar expBarUI;

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
    /// ���� Text
    /// </summary>
    [SerializeField]
    private TMP_Text levelText;

    /// <summary>
    /// ���� ü�¹�
    /// </summary>
    [SerializeField]
    private SliderBar bossHpBarUI;

    public GameOver GameOverUI;

    /// <summary>
    /// �Ͻ� ���� üũ��
    /// </summary>
    [HideInInspector]
    public bool IsPaused = false;

    private readonly Vector3 HpBarOffset = new Vector3(0, -50f, 0);

    public override void Init()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerManager.Instance.PlayerScript.Stat.OnLevelUp += ShowSkillSelectUI;
        UpdateHpBar(1f);
        ShowSkillSelectUI();
        UpdateSkillListUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowSkillSelectUI()
    {
        skillSelectUI.ShowUI();
        skillSelectUI.SetButton();
        levelText.text = "만 " + PlayerManager.Instance.PlayerScript.Stat.CurrentLevel.ToString() + "세";
    }

    public void UpdateSkillListUI()
    {
        hasSkillListUI.UpdateHasSkillList();
    }

    public void UpdateHpBar(float value)
    {
        hpBarUI.UpdateSliderBar(value);
    }

    public void UpdateExp()
    {
        var playerStat = PlayerManager.Instance.PlayerScript.Stat;

        if (playerStat != null)
        {
            expBarUI.UpdateSliderBar(Mathf.Clamp01((float)playerStat.CurrentEXP / playerStat.MaxEXP));
        }
    }

    public void UpdateTimer()
    {
        waveTimerUI.UpdateTimer();
    }

    public void SetActiveBossHpBar(bool isActive)
    {
        bossHpBarUI.gameObject.SetActive(isActive);
    }

    public void UpdateBossHpBar(float value)
    {
        bossHpBarUI.UpdateSliderBar(value);
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

    public void Finish()
    {
        PlayerManager.Instance.PlayerScript.Stat.OnLevelUp -= ShowSkillSelectUI;
    }
}
