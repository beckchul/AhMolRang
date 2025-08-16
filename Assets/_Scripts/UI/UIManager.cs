using System;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    /// <summary>
    /// 경험치 바
    /// </summary>
    [SerializeField]
    private ExpBar expBarUI;

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
    /// 일시 정지 체크용
    /// </summary>
    [HideInInspector]
    public bool IsPaused = false;


    public override void Init()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerManager.Instance.PlayerScript.Stat.OnLevelUp += ShowSkillSelectUI;

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
    }

    public void UpdateSkillListUI()
    {
        hasSkillListUI.UpdateHasSkillList();
    }

    public void UpdateExp()
    {
        expBarUI.UpdateExpBar();
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
