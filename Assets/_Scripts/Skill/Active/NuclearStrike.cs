using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class NuclearStrike : ActiveSkill
{
    [SerializeField]
    private DamageArea _damageAreaPrefab;
    [SerializeField]
    private GameObject _indicator;
    [SerializeField]
    private float _range = 10f;
    [SerializeField]
    private float _radius = 2f;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float firstDelay = 2f;
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
        _indicator.transform.parent = null;
        while (gameObject.activeSelf)
        {
            var target = MonsterManager.Instance.GetMonsterWithHighestHP(transform.position, _range);
            if (target)
            {
                PlaySound1();
                var pos = target.transform.position;
                _indicator.transform.position = pos;
                _indicator.SetActive(true);
                yield return new WaitForSeconds(firstDelay);
                yield return new WaitForSeconds(1.5f);
                _indicator.SetActive(false);
                var area = _damageAreaPool.Get();
                area.Summon(
                    pos,
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
        PlaySound2();
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
}
