using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    /// <summary>
    /// 플레이어 체력바
    /// </summary>
    [SerializeField]
    private SliderBar hpBarUI;

    /// <summary>
    /// 웨이브 타이머
    /// </summary>
    [SerializeField]
    private WaveTimer waveTimerUI;

    /// <summary>
    /// 경험치 바
    /// </summary>
    [SerializeField]
    private SliderBar expBarUI;

    /// <summary>
    /// 스킬 선택 창
    /// </summary>
    [SerializeField]
    private SkillSelect skillSelectUI;

    /// <summary>
    /// 가진 스킬 아이콘 관리용
    /// </summary>
    [SerializeField]
    private HasSkillList hasSkillListUI;

    /// <summary>
    /// 일시 정지 창
    /// </summary>
    [SerializeField]
    private GameObject pause;

    /// <summary>
    /// 레벨 Text
    /// </summary>
    [SerializeField]
    private TMP_Text levelText;

    /// <summary>
    /// 보스 체력바
    /// </summary>
    [SerializeField]
    private SliderBar bossHpBarUI;

    /// <summary>
    /// 일시 정지 체크용
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
    /// 일시 정지 버튼
    /// </summary>
    public void OnClickPauseButton()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        pause.SetActive(true);
    }

    /// <summary>
    /// 게임 재개 버튼
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
