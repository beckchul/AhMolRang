using System.Collections;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    [field: SerializeField]
    private float AttackDelay { get; set; } = 1f; // Attack delay in seconds

    private CharacterStat _stat;
    private GameObject _target;
    private Collider2D _collider;
    private bool _isContacting;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _stat = new CharacterStat();
        StartCoroutine(CoTick());
        _target = GameObject.FindWithTag("Player");
        _collider = GetComponent<Collider2D>();
        _isContacting = false;
    }

    private IEnumerator CoTick()
    {
        while (_stat.hp > 0)
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

    private IEnumerator CoMove(float duration)
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

    private IEnumerator CoProcessContact()
    {
        while (_isContacting)
        {
            Debug.Log($"{gameObject.name} is contacting with Player, attacking...");
            var delay = AttackDelay / _stat.attackSpeed;
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isContacting = true;
            StartCoroutine(CoProcessContact());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isContacting = false;
        }
    }
}
