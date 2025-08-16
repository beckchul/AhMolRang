using System.Collections;
using UnityEngine;

public class WheelWind : ActiveSkill
{
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public override void Use()
    {
        StartCoroutine(CoProcessEffect());
    }

    public IEnumerator CoProcessEffect()
    {
        var delay = new WaitForSeconds(Cooldown);

        while (gameObject.activeSelf)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _collider.radius, MonsterLayerMask);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out MonsterBase monster))
                {
                    var stat = monster.Stat;
                    int damage = Mathf.RoundToInt(stat.AttackDamage * Efficiency);
                    stat.TakeDamage(damage);
                }
            }

            yield return delay;
        }
    }
}
