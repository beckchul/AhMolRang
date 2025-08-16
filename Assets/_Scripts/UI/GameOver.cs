using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject defeat;
    public GameObject victory;

    public Button _resetBtn;

    private void Awake()
    {
        defeat.SetActive(false);
        victory.SetActive(false);
        _resetBtn.onClick.AddListener(GoToMainScene);
    }

    public void ShowDefeat()
    {
        gameObject.SetActive(true);
        defeat.SetActive(true);
        victory.SetActive(false);
    }

    public void ShowVictory()
    {
        gameObject.SetActive(true);
        defeat.SetActive(false);
        victory.SetActive(true);
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
