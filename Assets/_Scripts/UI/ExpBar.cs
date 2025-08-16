using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField]
    private Slider sliderBar;

    private void Awake()
    {
        sliderBar = GetComponent<Slider>();
    }

    public void UpdateExpBar()
    {
        var playerStat = PlayerManager.Instance.PlayerScript.Stat;

        if (playerStat != null)
        {
            sliderBar.value = Mathf.Clamp01((float)playerStat.CurrentEXP / playerStat.MaxEXP);
        }
    }
}
