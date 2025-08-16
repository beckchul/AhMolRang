using System;
using System.Collections;
using UnityEngine;

public class DelayedBomb : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D areaCollider;
    [SerializeField]
    private float delay = 2f;
    [SerializeField]
    private float speed = 3f;

    Vector3 position;
    Vector3 targetPosition;
    private float duration;
    private float detectRange;
    private int damage;

    private Action<DelayedBomb> onHit;
    private Action<DelayedBomb> onExpired;

    public void Summon(
        Vector3 position,
        Vector3 targetPosition,
        int damage,
        float duration,
        float radius,
        float detectRange,
        Action<DelayedBomb> onHit,
        Action<DelayedBomb> onExpired)
    {
        this.position = position;
        this.targetPosition = targetPosition;

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
        yield return StartCoroutine(CoThrow());

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

    private IEnumerator CoThrow()
    {
        var elapsedTime = 0f;
        var estimatedTime = Vector3.Distance(position, targetPosition) / speed;
        while (elapsedTime < estimatedTime)
        {
            transform.position = Vector3.Lerp(position, targetPosition, elapsedTime / estimatedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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
