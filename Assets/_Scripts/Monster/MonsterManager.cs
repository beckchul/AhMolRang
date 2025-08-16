using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    [SerializeField]
    private int _waveCount = 5;


    [SerializeField]
    private MonsterBase _monsterPrefab;
    private ObjectPool<MonsterBase> _monsterPool;
    private HashSet<MonsterBase> _activeMonsters = new();

    private WaitForSeconds _wait1Second = new(1f);
    private Coroutine _waveCoroutine;

    public MonsterBase CurrentBoss;

    public int BossExpMin = 30;
    public int BossExpMax = 100;
    public int WaveTime = 180;
    public int BossWaveTime = 30;
    public int ElapsedTime = 0;

    [Header("Ellipse Settings")]
    public float maxX = 10f; // X축 최대 반경 (타원의 가로 반지름)
    public float maxY = 6f;  // Y축 최대 반경 (타원의 세로 반지름)
    public float minX = 6f;  // X축 최소 반경 (안쪽 타원의 가로 반지름)
    public float minY = 4f;  // Y축 최소 반경 (안쪽 타원의 세로 반지름)

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

        _waveCoroutine = StartCoroutine(ProcessWave());
    }

    private IEnumerator ProcessWave()
    {
        for (int i = 0; i < _waveCount; ++i)
        {
            while (ElapsedTime < WaveTime)
            {
                SpawnMonster(OnMonsterDead, GetMonsterPosition());
                ++ElapsedTime;
                UIManager.Instance.UpdateTimer();
                yield return _wait1Second;
            }

            // BOSS WAVE
            ElapsedTime = 0;
            CurrentBoss = SpawnBoss(OnBossDead, GetMonsterPosition(), 0);

            while (ElapsedTime < BossWaveTime &&
                CurrentBoss.Stat.CurrentHP > 0)
            {
                SpawnMonster(OnMonsterDead, GetMonsterPosition());
                ++ElapsedTime;
                UIManager.Instance.UpdateTimer();
                yield return _wait1Second;
            }

            if (CurrentBoss.Stat.CurrentHP > 0)
            {
                Debug.Log("Failed to clear boss.");
                GameManager.Instance.Defeat();
                FinishGame();
                yield break;
            }
        }

        Debug.Log("All waves completed!");
        GameManager.Instance.Victory();
        FinishGame();
    }
    
    public void FinishGame()
    {
        foreach (var monster in _activeMonsters)
        {
            monster.gameObject.SetActive(false);
            _monsterPool.Release(monster);
        }

        _activeMonsters.Clear();
        StopCoroutine(_waveCoroutine);
    }

    private void OnMonsterDead(MonsterBase monster)
    {
        ExpManager.Instance.DropExp(monster.gameObject.transform.position);
        _monsterPool.Release(monster);
        ExpManager.Instance.DropExp(monster.transform.position);
    }

    private void OnBossDead(MonsterBase monster)
    {
        _monsterPool.Release(monster);
        var amount = Random.Range(BossExpMin, BossExpMax + 1);

        for (int i = 0; i < amount; ++i)
        {
            var rndX = Random.Range(-1f, 1f);
            var rndY = Random.Range(-1f, 1f);
            var dropPosition = monster.transform.position + new Vector3(rndX, rndY);
            ExpManager.Instance.DropExp(dropPosition);
        }
    }

    private void OnGetMonster(MonsterBase monster)
    {
        _activeMonsters.Add(monster);
        monster.gameObject.SetActive(false);
    }

    private void OnReleaseMonster(MonsterBase monster)
    {
        _activeMonsters.Remove(monster);
        monster.gameObject.SetActive(false);
    }

    private MonsterBase CreateMonster()
    {
        return Instantiate(_monsterPrefab, transform);
    }

    private void OnDestroyMonster(MonsterBase monster)
    {
        Destroy(monster.gameObject);
    }

    public MonsterBase SpawnBoss(Action<MonsterBase> onDead, Vector3 position, int spriteId)
    {
        var monster = _monsterPool.Get();
        monster.transform.position = position;
        monster.Set(onDead);

        return monster;
    }

    public MonsterBase SpawnMonster(Action<MonsterBase> onDead, Vector3 position)
    {
        var monster = _monsterPool.Get();
        monster.transform.position = position;
        monster.Set(onDead);

        return monster;
    }

    private Vector3 GetMonsterPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // 각도별 외곽 반경
        float outerRadiusX = maxX;
        float outerRadiusY = maxY;

        // 각도별 내부 반경
        float innerRadiusX = minX;
        float innerRadiusY = minY;

        // 랜덤 반경 (내부와 외부 사이)
        float t = Random.Range(0f, 1f);
        float radiusX = Mathf.Lerp(innerRadiusX, outerRadiusX, t);
        float radiusY = Mathf.Lerp(innerRadiusY, outerRadiusY, t);

        // 좌표 변환 (타원 공식)
        float x = Mathf.Cos(angle) * radiusX;
        float y = Mathf.Sin(angle) * radiusY;

        return PlayerManager.Instance.PlayerScript.transform.position + new Vector3(x, y);
    }
}
