using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class AngryBird : ActiveSkill
{
    [SerializeField]
    private PierceProjectile[] _projectilePrefab;
    [SerializeField]
    private float _range = 7f;
    [SerializeField]
    private float _duration = 1f;

    private ObjectPool<PierceProjectile> _projectilePool;

    private void Awake()
    {
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        _projectilePool = new ObjectPool<PierceProjectile>(
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
                projectile.PierceShoot(
                    target.transform.position - transform.position,
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

    private void OnProjectileHit(PierceProjectile projectile)
    {
    }

    private void OnProjectileExpired(PierceProjectile projectile)
    {
        _projectilePool.Release(projectile);
    }

    private PierceProjectile CreateProjectile()
    {
        return Instantiate(_projectilePrefab[Random.Range(0, _projectilePrefab.Length)], transform.position, Quaternion.identity);
    }

    private void OnGetProjectile(PierceProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnReleaseProjectile(PierceProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(PierceProjectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}
