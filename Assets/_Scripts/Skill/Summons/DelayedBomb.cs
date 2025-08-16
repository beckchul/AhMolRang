using System;
using System.Collections;
using UnityEngine;

public class DelayedBomb : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D areaCollider;
    [SerializeField]
    private float delay = 2f;

    private float duration;
    private float detectRange;
    private int damage;

    private Action<DelayedBomb> onHit;
    private Action<DelayedBomb> onExpired;

    public void Summon(
        Vector3 position,
        int damage,
        float duration,
        float radius,
        float detectRange,
        Action<DelayedBomb> onHit,
        Action<DelayedBomb> onExpired)
    {
        transform.position = position;
        this.damage = damage;
        this.duration = duration;
        this.detectRange = detectRange;
        this.onHit = onHit;
        this.onExpired = onExpired;

        areaCollider.radius = radius;
        gameObject.SetActive(true);
        StartCoroutine(CoTick());
    }

    private IEnumerator CoTick()
    {
        var wait = new WaitForSeconds(delay);
        var elapsedTime = 0f;
        while (duration > elapsedTime)
        {
            if (MonsterManager.Instance.GetNearestMonster(transform.position, detectRange))
            {
                yield return wait;
                ApplyEffect();
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
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
            }
        }

        onHit?.Invoke(this);
    }
}
