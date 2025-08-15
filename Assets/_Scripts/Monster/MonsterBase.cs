using System.Collections;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    private CharacterStat _stat;
    private GameObject _target;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _stat = new CharacterStat();
        StartCoroutine(CoTick());
        _target = GameObject.FindWithTag("Player");
    }

    public IEnumerator CoTick()
    {
        while(_stat.hp > 0)
        {
            var tickDuration = Random.Range(0.8f, 1.2f);
            if (_target != null)
            {
                yield return StartCoroutine(CoMove(tickDuration));
            }
            else
            {
                yield return new WaitForSeconds(tickDuration);
            }
        }
    }

    public IEnumerator CoMove(float duration)
    {
        // Simulate moving towards the target
        var elapsedTime = 0f;

        while (duration > elapsedTime)
        {
            var direction = (_target.transform.position - transform.position).normalized;
            transform.position += _stat.moveSpeed * Time.deltaTime * direction;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
