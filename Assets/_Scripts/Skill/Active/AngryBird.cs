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

    [SerializeField]
    private int yellowDamage = 150;
    [SerializeField]
    private int redDamage = 100;

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

                var randomIndex = Random.Range(0, _projectilePrefab.Length);
                var damage = randomIndex == 0 ? this.damage :
                    (randomIndex == 1 ? yellowDamage : redDamage);

                var projectile = Instantiate(_projectilePrefab[randomIndex], transform.position, Quaternion.identity);
                projectile.transform.position = transform.position;
                projectile.PierceShoot(
                    target.transform.position - transform.position,
                    damage,
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
