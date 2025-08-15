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
    private Stack<MonsterBase> _monsterStack = new();

    private WaitForSeconds _wait1Second = new(1f);
    private Coroutine _waveCoroutine;

    [Header("Ellipse Settings")]
    public float maxX = 10f; // X�� �ִ� �ݰ� (Ÿ���� ���� ������)
    public float maxY = 6f;  // Y�� �ִ� �ݰ� (Ÿ���� ���� ������)
    public float minX = 6f;  // X�� �ּ� �ݰ� (���� Ÿ���� ���� ������)
    public float minY = 4f;  // Y�� �ּ� �ݰ� (���� Ÿ���� ���� ������)

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
        var waveTime = 180;
        var elapsedTime = 0;

        for (int i = 0; i < _waveCount; ++i)
        {
            while (elapsedTime < waveTime)
            {
                SpawnMonster(OnMonsterDead, GetMonsterPosition(), 0);
                ++elapsedTime;
                yield return _wait1Second;
            }

            // BOSS WAVE
            elapsedTime = 0;
            var boss = SpawnBoss(OnMonsterDead, GetMonsterPosition(), 0);

            while (elapsedTime < waveTime &&
                boss.Stat.CurrentHP > 0)
            {
                SpawnMonster(OnMonsterDead, GetMonsterPosition(), 0);
                ++elapsedTime;
                yield return _wait1Second;
            }

            if (boss.Stat.CurrentHP > 0)
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
        while(_monsterStack.Count > 0)
        {
            var monster = _monsterStack.Pop();
            monster.gameObject.SetActive(false);
        }

        StopCoroutine(_waveCoroutine);
    }

    private void OnMonsterDead(MonsterBase monster)
    {
        _monsterPool.Release(monster);
    }

    private void OnGetMonster(MonsterBase monster)
    {
        monster.gameObject.SetActive(true);
        _monsterStack.Push(monster);
    }

    private void OnReleaseMonster(MonsterBase monster)
    {
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

    public MonsterBase SpawnMonster(Action<MonsterBase> onDead, Vector3 position, ThemeType themeType)
    {
        var monster = _monsterPool.Get();
        monster.transform.position = position;
        monster.Set(onDead);

        return monster;
    }

    private Vector3 GetMonsterPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // ������ �ܰ� �ݰ�
        float outerRadiusX = maxX;
        float outerRadiusY = maxY;

        // ������ ���� �ݰ�
        float innerRadiusX = minX;
        float innerRadiusY = minY;

        // ���� �ݰ� (���ο� �ܺ� ����)
        float t = Random.Range(0f, 1f);
        float radiusX = Mathf.Lerp(innerRadiusX, outerRadiusX, t);
        float radiusY = Mathf.Lerp(innerRadiusY, outerRadiusY, t);

        // ��ǥ ��ȯ (Ÿ�� ����)
        float x = Mathf.Cos(angle) * radiusX;
        float y = Mathf.Sin(angle) * radiusY;

        return PlayerManager.Instance.PlayerScript.transform.position + new Vector3(x, y);
    }
}
