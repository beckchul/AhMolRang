using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private int damage;

    private Vector3 direction;
    private List<MonsterBase> monsterList = new();
    private Action<PierceProjectile> onHit;
    private Action<PierceProjectile> onExpired;
    private float duration;

    public void PierceShoot(
        Vector3 direction,
        int damage,
        float duration,
        Action<PierceProjectile> onHit,
        Action<PierceProjectile> onExpired)
    {
        this.direction = direction.normalized;
        this.duration = duration;
        this.onHit = onHit;
        this.onExpired = onExpired;
        this.damage = damage;
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
            foreach (var attackedMonster in monsterList)
            {
                if (attackedMonster == null || monster == attackedMonster)
                {
                    return;
                }
            }

            monsterList.Add(monster);
            monster.Stat.TakeDamage(damage);
        }
    }

    public void LookAtDirection(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
