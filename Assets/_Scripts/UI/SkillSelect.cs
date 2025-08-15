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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        foreach (SkillSelectButton button in skillSelectButtons)
        {
            button.OnClicked -= OnSkillSelect;
        }
    }

    public void OnSkillSelect(int skillType, int skillId, int skillLevel)
    {

    }
}
