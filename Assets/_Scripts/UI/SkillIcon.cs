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
        // ��ų ������ �� text ����
        skillImage.sprite = sprite;
        skillText.text = "Lv." + level;
    }
}
