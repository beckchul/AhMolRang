using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class HeavyMachinegun : ActiveSkill
{
    [SerializeField]
    private Projectile _projectilePrefab;
    [SerializeField]
    private float _range = 7.5f;
    [SerializeField]
    private float _duration = 0.75f;

    private ObjectPool<Projectile> _projectilePool;

    private void Awake()
    {
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        _projectilePool = new ObjectPool<Projectile>(
            CreateProjectile,
            OnGetProjectile,
            OnReleaseProjectile,
            OnDestroyProjectile,
            maxSize: 100
        );

        StartCoroutine(CoProcessEffect());
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            var target = MonsterManager.Instance.GetNearestMonster(transform.position, _range);
            if (target)
            {
                var projectile = _projectilePool.Get();
                projectile.transform.position = transform.position;
                projectile.Shoot(
                    target.transform.position - transform.position,
                    15,
                    _duration,
                    OnProjectileHit,
                    OnProjectileExpired
                );
                yield return new WaitForSeconds(Cooldown);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnProjectileHit(Projectile projectile)
    {
        _projectilePool.Release(projectile);
    }

    private void OnProjectileExpired(Projectile projectile)
    {
        _projectilePool.Release(projectile);
    }

    private Projectile CreateProjectile()
    {
        return Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
    }

    private void OnGetProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnReleaseProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}
