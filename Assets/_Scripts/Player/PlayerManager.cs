using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [HideInInspector]
    public Player PlayerScript;
    [HideInInspector]
    public GameObject PlayerObject;

    [SerializeField]
    private GameObject playerObject;

    public override void Init()
    {
        PlayerObject = Instantiate(playerObject, Vector2.zero, Quaternion.identity);

        if (PlayerObject.TryGetComponent<Player>(out PlayerScript))
        {
            PlayerScript.InitPlayer();
        }
    }
}
