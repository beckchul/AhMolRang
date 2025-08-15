using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;

    private Vector2 movementDirection = Vector2.zero;


    public void Init(Player _player)
    {
        player = _player;
        player.Controller.OnMoveEvent += Move;
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
        direction *= player.Stat.MoveSpeed;

        player.Rigidbody2D.linearVelocity = direction;
    }
}
