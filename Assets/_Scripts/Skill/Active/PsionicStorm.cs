using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PsionicStorm : ActiveSkill
{
    [SerializeField]
    private DamageArea _damageAreaPrefab;
    [SerializeField]
    private float _range = 10f;
    [SerializeField]
    private float _radius = 2f;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float tickDelay;

    private ObjectPool<DamageArea> _damageAreaPool;

    private void Awake()
    {
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        _damageAreaPool = new ObjectPool<DamageArea>(
            CreateArea,
            OnGetArea,
            OnReleaseArea,
            OnDestroyArea,
            maxSize: 100
        );

        StartCoroutine(CoProcessEffect());
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            var target = MonsterManager.Instance.GetRandomMonster(transform.position, _range);
            if (target)
            {
                RandomPlaySound();
                var area = _damageAreaPool.Get();
                area.Summon(
                    target.transform.position,
                    damage,
                    duration,
                    _radius,
                    tickDelay,
                    OnAreaHit,
                    OnAreaExpired
                );
                yield return new WaitForSeconds(Cooldown);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnAreaHit(DamageArea projectile)
    {
    }

    private void OnAreaExpired(DamageArea projectile)
    {
        _damageAreaPool.Release(projectile);
    }

    private DamageArea CreateArea()
    {
        return Instantiate(_damageAreaPrefab, transform.position, Quaternion.identity);
    }

    private void OnGetArea(DamageArea projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnReleaseArea(DamageArea projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyArea(DamageArea projectile)
    {
        Destroy(projectile.gameObject);
    }

    private void RandomPlaySound()
    {
        int ran = Random.Range(1, 3);

        if (ran == 1) PlaySound1(0.7f);
        else PlaySound2(0.7f);
    }
}
