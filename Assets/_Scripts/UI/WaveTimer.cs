using TMPro;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;
   
    public void UpdateTimer()
    {
        var waveTime = MonsterManager.Instance.IsBossWave ? 
            MonsterManager.Instance.BossWaveTime : MonsterManager.Instance.WaveTime;
        int sec = Mathf.Max(waveTime - (int)MonsterManager.Instance.ElapsedTime, 0);
        int min = sec / 60;
        sec = sec % 60;

        string secStr = sec < 10 ? "0" + sec.ToString() : sec.ToString();

        timerText.text = min.ToString() + " : " + secStr;
    }
}
