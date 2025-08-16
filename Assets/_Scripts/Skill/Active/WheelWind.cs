using System.Collections;
using UnityEngine;

public class WheelWind : ActiveSkill
{
    [SerializeField]
    private CircleCollider2D _collider;
    [SerializeField]
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer.enabled = false;
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        _renderer.enabled = true;
        StartCoroutine(CoProcessEffect());
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _collider.radius, MonsterLayerMask);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out MonsterBase monster))
                {
                    var stat = monster.Stat;
                    //int damage = Mathf.RoundToInt(stat.AttackDamage * Efficiency);
                    stat.TakeDamage(100);
                }
            }

            yield return new WaitForSeconds(Cooldown);
        }
    }
}
