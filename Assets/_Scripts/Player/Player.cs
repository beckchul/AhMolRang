using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    public CharacterStat Stat { get; private set; }


    public void InitPlayer()
    {
        Controller = GetComponent<PlayerController>();
        Movement = GetComponent<PlayerMovement>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

        Stat = new CharacterStat();

        Rigidbody2D.gravityScale = 0.0f;

        Controller.Init(this);
        Movement.Init(this);
    }

    private void Start()
    {
        InitPlayer();
    }
}
