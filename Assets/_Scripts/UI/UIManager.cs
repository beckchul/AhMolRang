using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private SkillSelect SkillSelectUI;

    [SerializeField]
    private HasSkillList HasSkillListUI;

    private void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowSkillSelectUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowSkillSelectUI()
    {
        SkillSelectUI.gameObject.SetActive(true);
    }

    public void UpdateSkillListUI()
    {
        
    }
}
