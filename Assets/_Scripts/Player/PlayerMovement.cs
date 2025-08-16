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
        Vector3 pos = transform.position;
        if (pos.x >= 23.0f) pos.x = 23.0f;
        if (pos.x <= -23.0f) pos.x = -23.0f;
        if (pos.y >= 23.0f) pos.y = 23.0f;
        if (pos.y <= -23.0f) pos.y = -23.0f;
        transform.position = pos;

        if (direction.x < 0) FlipPlayer(true);
        else FlipPlayer(false);
    }

    private void FlipPlayer(bool right)
    {
        Vector3 scale = player.SpriteRenderer.transform.localScale;
        scale.x = right ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        player.SpriteRenderer.transform.localScale = scale;
    }
}
