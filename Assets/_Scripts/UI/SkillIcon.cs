using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField]
    private Image skillImage;

    [SerializeField]
    private TMP_Text skillText;

    public void UpdateIcon(Sprite sprite, int level)
    {
        // 스킬 아이콘 및 text 갱신
        skillImage.sprite = sprite;
        skillText.text = "Lv." + level;
    }
}
