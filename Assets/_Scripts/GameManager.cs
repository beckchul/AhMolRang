using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

    public void Victory()
    {
        Debug.Log("Victory!");
        UIManager.Instance.GameOverUI.ShowVictory();
    }

    public void Defeat()
    {
        Debug.Log("Defeat!");
        UIManager.Instance.GameOverUI.ShowDefeat();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("SSG");
    }
}
