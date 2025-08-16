using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class MagicClaw : ActiveSkill
{
    [SerializeField]
    private MagicClawEffect _magicClawEffectPrefab;
    [SerializeField]
    private float _range = 6.0f;

    private MonsterBase monster;
    private int damage = 70;

    private ObjectPool<MagicClawEffect> _magicClawPool;


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
                PlaySound1();

                monster = target;

                var magicClawEffect = _magicClawPool.Get();
                magicClawEffect.transform.position = target.transform.position;
                StartCoroutine(Attack_Coroutine(magicClawEffect));
                yield return new WaitForSeconds(Cooldown);
            }
            else
            {
                monster = null;

                yield return null;
            }
        }
    }

    private void OnMagicClawEffectHit(MagicClawEffect magicClawEffect)
    {
        int value = (int)(damage / 2 + 0.5f);
        monster.Stat.TakeDamage(value);

        PlaySound2();
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

    IEnumerator Attack_Coroutine(MagicClawEffect effect)
    {
        yield return new WaitForSeconds(0.5f);
        effect.Shoot(monster, OnMagicClawEffectHit, OnMagicClawEffectExpired);
    }
}
