using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    public void Victory()
    {
        Debug.Log("Victory!");
    }

    public void Defeat()
    {
        Debug.Log("Defeat!");
    }
}
