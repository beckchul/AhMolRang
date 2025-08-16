using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class MagicClaw : ActiveSkill
{
    [SerializeField]
    private MagicClawEffect _magicClawEffectPrefab;
    [SerializeField]
    private DamageArea _genesisEffectPrefab;
    [SerializeField]
    private Animator _genesisVFX;
    [SerializeField]
    private float _range = 6.0f;
    [SerializeField]
    private int _genesisPillarCount = 15;
    [SerializeField]
    private float _genesisRadius = 5.0f;
    [SerializeField]
    private float _genesisTime = 1.0f;
    [SerializeField]
    private float _genesisExpireTime = 1.5f;
    [SerializeField]
    private float _genesisDelay = 0.5f;

    private MonsterBase monster;

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
                if (Level < 6)
                {
                    CastMagicClaw(target);
                }
                else
                {
                    yield return StartCoroutine(CastGenesis());
                }

                yield return new WaitForSeconds(Cooldown);
            }
            else
            {
                monster = null;

                yield return null;
            }
        }
    }

    private void CastMagicClaw(MonsterBase target)
    {
        PlaySound1();

        monster = target;

        var magicClawEffect = _magicClawPool.Get();
        magicClawEffect.transform.position = target.transform.position;
        StartCoroutine(Attack_Coroutine(magicClawEffect));
    }

    private IEnumerator CastGenesis()
    {
        PlaySound(1321);
        _genesisVFX.Play("GenesisPre");
        yield return new WaitForSeconds(_genesisDelay);

        for (int i = 0; i < _genesisPillarCount; i++)
        {
            var rndX = Random.Range(-1.0f, 1.0f);
            var rndY = Random.Range(-1.0f, 1.0f);
            var position = transform.position + new Vector3(rndX, rndY, 0) * _range * 2;

            var genesisEffect = Instantiate(_genesisEffectPrefab, position, Quaternion.identity);
            genesisEffect.Summon(
                position, damage, _genesisTime, _genesisRadius, _genesisExpireTime, OnGenesisHit, OnGenesisExpired);
        }
        PlaySound(1322);
    }

    private void OnGenesisHit(DamageArea damageArea)
    {
    }

    private void OnGenesisExpired(DamageArea damageArea)
    {
        Destroy(damageArea.gameObject);
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
