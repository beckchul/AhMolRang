using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SummonTNT: ActiveSkill
{
    [SerializeField]
    private DelayedBomb _bombPrefab;
    [SerializeField]
    private float _range = 8f;
    [SerializeField]
    private float _detectRange = 1.5f;
    [SerializeField]
    private float _duration = 15f;
    [SerializeField]
    private float _radius = 2f;

    private ObjectPool<DelayedBomb> _bombPool;

    private void Awake()
    {
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        _bombPool = new ObjectPool<DelayedBomb>(
            CreateProjectile,
            OnGetBomb,
            OnReleaseBomb,
            OnDestroyBomb,
            maxSize: 100
        );

        StartCoroutine(CoProcessEffect());
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            var rndX = Random.Range(-_range, _range);
            var rndY = Random.Range(-_range, _range);
            Debug.Log($"SummonTNT: Random position offset: ({rndX}, {rndY})");
            var targetPosition = transform.position + new Vector3(rndX, rndY);

            var bomb = _bombPool.Get();
            bomb.Summon(
                transform.position,
                targetPosition,
                200,
                _duration,
                _radius,
                _detectRange,
                OnBombHit,
                OnBombExpired
            );
            yield return new WaitForSeconds(Cooldown);
        }
    }

    private void OnBombHit(DelayedBomb projectile)
    {
        _bombPool.Release(projectile);
    }

    private void OnBombExpired(DelayedBomb projectile)
    {
        _bombPool.Release(projectile);
    }

    private DelayedBomb CreateProjectile()
    {
        return Instantiate(_bombPrefab, transform.position, Quaternion.identity);
    }

    private void OnGetBomb(DelayedBomb bomb)
    {
        bomb.gameObject.SetActive(false);
    }

    private void OnReleaseBomb(DelayedBomb bomb)
    {
        bomb.gameObject.SetActive(false);
    }

    private void OnDestroyBomb(DelayedBomb bomb)
    {
        Destroy(bomb.gameObject);
    }
}
