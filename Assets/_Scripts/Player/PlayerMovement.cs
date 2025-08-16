using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    private Player player;

    private Vector2 movementDirection = Vector2.zero;


    public void Init(Player _player)
    {
        player = _player;
        player.Controller.OnMoveEvent += Move;
    }

    private void Update()
    {
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        transform.position += (Vector3)direction * Time.deltaTime * player.Stat.MoveSpeed;
    }

    private void FlipPlayer()
    {
        //player.
    }
}
