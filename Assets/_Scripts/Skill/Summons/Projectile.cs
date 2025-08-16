using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.75f;
    [SerializeField]
    private float speed = 10f;
    private int damage;

    private Vector3 direction;
    private Action<Projectile> onHit;
    private Action<Projectile> onExpired;

    public void Shoot(
        Vector3 direction,
        int damage,
        Action<Projectile> onHit,
        Action<Projectile> onExpired)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        this.onHit = onHit;
        this.onExpired = onExpired;
        LookAtDirection(direction);

        gameObject.SetActive(true);
        StartCoroutine(CoTick());
    }

    private IEnumerator CoTick()
    {
        var elapsedTime = 0f;
        while (duration > elapsedTime)
        {
            transform.position += speed * Time.deltaTime * direction;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (elapsedTime >= duration)
        {
            onExpired?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out var monster))
        {
            monster.Stat.TakeDamage(damage);
            onHit?.Invoke(this);
        }
    }

    public void LookAtDirection(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
