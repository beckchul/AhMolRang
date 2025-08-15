using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UpdateIcon(int skillType, int skillId, int skillLevel)
    {
        // 스킬 아이콘 및 text 갱신
    }
}
