using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController controller;
    private Rigidbody2D rb;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        // 나중에 player를 받아와 player에 있는 controller와 rb를 받아와야 한다.
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;

        //rb.velocity = direction;
        rb.linearVelocity = direction;
    }
}
