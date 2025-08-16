using TMPro;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;
   
    public void UpdateTimer()
    {
        int sec = Mathf.Max(MonsterManager.Instance.WaveTime - (int)MonsterManager.Instance.ElapsedTime, 0);
        int min = sec / 60;
        sec = sec % 60;

        timerText.text = min.ToString() + " : " + sec.ToString();
    }
}
