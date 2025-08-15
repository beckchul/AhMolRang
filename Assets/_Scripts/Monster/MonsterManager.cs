using System;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    [SerializeField]
    private MonsterBase _monsterPrefab;
    private ObjectPool<MonsterBase> _monsterPool;

    private void Awake()
    {
        _monsterPool = new ObjectPool<MonsterBase>(
            CreateMonster,
            OnGetMonster,
            OnReleaseMonster,
            OnDestroyMonster,
            false, // collection check
            50, // initial capacity
            200 // max size
        );

        for (int i = 0; i < 5; i++)
        {
            var rndx = UnityEngine.Random.Range(-10f, 10f);
            var rndy = UnityEngine.Random.Range(-10f, 10f);

            SpawnMonster(new Vector3(rndx, rndy), 0);
        }
    }

    private void OnGetMonster(MonsterBase monster)
    {
        monster.gameObject.SetActive(true);
    }

    private void OnReleaseMonster(MonsterBase monster)
    {
        monster.gameObject.SetActive(false);
    }

    private MonsterBase CreateMonster()
    {
        return Instantiate(_monsterPrefab);
    }

    private void OnDestroyMonster(MonsterBase monster)
    {
        Destroy(monster.gameObject);
    }

    public void SpawnMonster(Vector3 position, int spriteId)
    {
        var monster = _monsterPool.Get();
        monster.transform.position = position;

        monster.Stat.Reset();
    }
}
