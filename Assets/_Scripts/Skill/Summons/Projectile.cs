using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private int damage;

    private Vector3 direction;
    private Action<Projectile> onHit;
    private Action<Projectile> onExpired;
    private float duration;

    public void Shoot(
        Vector3 direction,
        int damage,
        float duration,
        Action<Projectile> onHit,
        Action<Projectile> onExpired)
    {

        var rndX = Random.Range(-0.1f, 0.1f);
        var rndY = Random.Range(-0.1f, 0.1f);
        var rndDirection = new Vector3(rndX, rndY);

        direction = (direction.normalized + rndDirection);

        this.direction = direction.normalized;
        this.damage = damage;
        this.duration = duration;
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
            onHit = null;
        }
    }

    public void LookAtDirection(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
