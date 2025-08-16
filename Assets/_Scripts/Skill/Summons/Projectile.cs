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

    private Vector2 direction;
    private Action<Projectile> onHit;
    private Action<Projectile> onExpired;

    public void Shoot(
        Vector2 direction,
        int damage,
        Action<Projectile> onHit,
        Action<Projectile> onExpired)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        this.onHit = onHit;
        this.onExpired = onExpired;

        gameObject.SetActive(true);
        StartCoroutine(CoTick());
    }

    private IEnumerator CoTick()
    {
        var elapsedTime = 0f;
        while (duration > elapsedTime)
        {
            transform.Translate(direction * speed * Time.deltaTime);
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
}
