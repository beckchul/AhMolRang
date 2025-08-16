using System;
using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D areaCollider;

    private float duration;
    private int damage;
    private float tickDelay;

    private Action<DamageArea> onHit;
    private Action<DamageArea> onExpired;

    public void Summon(
        Vector3 position,
        int damage,
        float duration,
        float radius,
        float tickDelay,
        Action<DamageArea> onHit,
        Action<DamageArea> onExpired)
    {
        transform.position = position;
        this.damage = damage;
        this.duration = duration;
        this.tickDelay = tickDelay;
        this.onHit = onHit;
        this.onExpired = onExpired;

        areaCollider.radius = radius;
        gameObject.SetActive(true);
        StartCoroutine(CoTick());
    }

    private IEnumerator CoTick()
    {
        var delay = new WaitForSeconds(tickDelay);
        var elapsedTime = 0f;
        while (duration > elapsedTime)
        {
            ApplyEffect();
            elapsedTime += tickDelay;
            yield return delay;
        }

        onExpired?.Invoke(this);
    }

    private void ApplyEffect()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, areaCollider.radius, LayerMask.GetMask("Enemy"));
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<MonsterBase>(out var monster))
            {
                monster.Stat.TakeDamage(damage);
                onHit?.Invoke(this);
            }
        }
    }
}
