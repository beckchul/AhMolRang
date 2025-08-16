using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MagicClaw : ActiveSkill
{
    [SerializeField]
    private MagicClawEffect _magicClawEffectPrefab;
    [SerializeField]
    private float _range = 6.0f;

    private ObjectPool<MagicClawEffect> _magicClawPool;

    private void Awake()
    {
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        _magicClawPool = new ObjectPool<MagicClawEffect>(
            CreateMagicClawEffect,
            OnGetMagicClawEffect,
            OnReleaseMagicClawEffect,
            OnDestroyMagicClawEffect,
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
                var magicClawEffect = _magicClawPool.Get();
                magicClawEffect.transform.position = target.transform.position;
                magicClawEffect.Shoot(70, target, OnMagicClawEffectHit, OnMagicClawEffectExpired);
                yield return new WaitForSeconds(Cooldown);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnMagicClawEffectHit(MagicClawEffect magicClawEffect)
    {
        _magicClawPool.Release(magicClawEffect);
    }

    private void OnMagicClawEffectExpired(MagicClawEffect magicClawEffect)
    {
        _magicClawPool.Release(magicClawEffect);
    }

    private MagicClawEffect CreateMagicClawEffect()
    {
        return Instantiate(_magicClawEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnGetMagicClawEffect(MagicClawEffect magicClawEffect)
    {
        magicClawEffect.gameObject.SetActive(false);
    }

    private void OnReleaseMagicClawEffect(MagicClawEffect magicClawEffect)
    {
        magicClawEffect.gameObject.SetActive(false);
    }

    private void OnDestroyMagicClawEffect(MagicClawEffect magicClawEffect)
    {
        Destroy(magicClawEffect.gameObject);
    }
}
