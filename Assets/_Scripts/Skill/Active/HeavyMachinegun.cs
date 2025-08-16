using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class HeavyMachinegun : ActiveSkill
{
    [SerializeField]
    private Projectile _projectilePrefab;
    [SerializeField]
    private Transform _gun;
    [SerializeField]
    private Transform _gunPoint;
    [SerializeField]
    private float _range = 7.5f;
    [SerializeField]
    private float _duration = 0.75f;

    private ObjectPool<Projectile> _projectilePool;

    private void Awake()
    {
    }

    private void Update()
    {
        if (Time.timeScale < 0.1f) StopSound(sound_ID_2);
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

        _gun.gameObject.SetActive(true);
        StartCoroutine(CoProcessEffect());
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            var target = MonsterManager.Instance.GetNearestMonster(transform.position, _range);
            if (target)
            {
                if (!PlayingSound) PlayAllSound(0.8f);

                var projectile = _projectilePool.Get();

                var direction = target.transform.position - transform.position;
                Projectile.LookAtDirection(direction, _gun);

                projectile.transform.position = _gunPoint.position;
                projectile.Shoot(
                    target.transform.position - _gunPoint.position,
                    damage,
                    _duration,
                    OnProjectileHit,
                    OnProjectileExpired
                );
                yield return new WaitForSeconds(Cooldown);
            }
            else
            {
                StopSound(sound_ID_2);
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
