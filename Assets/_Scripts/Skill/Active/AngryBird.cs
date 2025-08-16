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
    private int sound_ID_3_Shot;
    [SerializeField]
    private int sound_ID_4_Fly;
    [SerializeField]
    private int sound_ID_5_Fly;
    [SerializeField]
    private int sound_ID_6_Fly;
    [SerializeField]
    private int sound_ID_7_Charge;


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

        SoundManager.Instance.PlaySFX(sound_ID_7_Charge);
        StartCoroutine(CoProcessEffect());
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            var target = MonsterManager.Instance.GetNearestMonster(transform.position, _range);
            if (target)
            {
                RandomPlayShotSound(0.8f);
                RandomPlayFlySound(0.8f);
                var projectile = _projectilePool.Get();
                projectile.transform.position = transform.position;
                projectile.PierceShoot(
                    target.transform.position - transform.position,
                    _duration,
                    OnProjectileHit,
                    OnProjectileExpired
                );
                SoundManager.Instance.PlaySFX(sound_ID_7_Charge);
                //StartCoroutine(CoProcessEffect());
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
        SoundManager.Instance.PlaySFX(sound_ID_7_Charge);
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

    private void RandomPlayShotSound(float volume = 1.0f)
    {
        int ranNum = Random.Range(1, 4);
        switch (ranNum)
        {
            case 1:
                PlaySound1();
                break;
            case 2:
                PlaySound2();
                break;
            case 3:
                SoundManager.Instance.PlaySFX(sound_ID_3_Shot);
                break;
            default:
                break;
        }
    }

    private void RandomPlayFlySound(float volume = 1.0f)
    {
        int ranNum = Random.Range(1, 4);
        switch (ranNum)
        {
            case 1:
                SoundManager.Instance.PlaySFX(sound_ID_4_Fly);
                break;
            case 2:
                SoundManager.Instance.PlaySFX(sound_ID_5_Fly);
                break;
            case 3:
                SoundManager.Instance.PlaySFX(sound_ID_6_Fly);
                break;
            default:
                break;
        }
    }
}
