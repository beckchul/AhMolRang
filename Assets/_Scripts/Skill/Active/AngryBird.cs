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

    [SerializeField]
    private int sound_ID_3;
    [SerializeField]
    private int sound_ID_4;
    [SerializeField]
    private int sound_ID_5;
    [SerializeField]
    private int sound_ID_6;


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
                RandomPlaySound();
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

    private void RandomPlaySound()
    {
        int ranNum = Random.Range(1, 7);
        switch (ranNum)
        {
            case 1:
                PlaySound1();
                break;
            case 2:
                PlaySound2();
                break;
            case 3:
                SoundManager.Instance.PlaySFX(sound_ID_3);
                break;
            case 4:
                SoundManager.Instance.PlaySFX(sound_ID_4);
                break;
            case 5:
                SoundManager.Instance.PlaySFX(sound_ID_5);
                break;
            case 6:
                SoundManager.Instance.PlaySFX(sound_ID_6);
                break;
            default:
                break;
        }
    }
}
