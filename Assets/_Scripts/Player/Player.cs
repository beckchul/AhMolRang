using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    public PlayerStat Stat { get; private set; }

    private void Awake()
    {
        InitPlayer();
    }

    public void InitPlayer()
    {
        Controller = GetComponent<PlayerController>();
        Movement = GetComponent<PlayerMovement>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

        Stat = new PlayerStat();
        Stat.OnDeath = () =>
        {
            MonsterManager.Instance.FinishGame();
            GameManager.Instance.Defeat();
        };
        Rigidbody2D.gravityScale = 0.0f;
    }
}
