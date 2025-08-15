using System;
using System.Collections;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    [field: SerializeField]
    private float AttackDelay { get; set; } = 1f; // Attack delay in seconds

    public CharacterStat Stat { get; private set; }

    private Player _target;
    private Collider2D _collider;
    private bool _isContacting;

    public void Set(Action<MonsterBase> onDead, Player target)
    {
        Stat = new CharacterStat();
        StartCoroutine(CoTick());
        _target = target;
        _collider = GetComponent<Collider2D>();
        _isContacting = false;
        Stat.OnDeath = () => onDead?.Invoke(this);
    }

    private IEnumerator CoTick()
    {
        while (Stat.CurrentHP > 0)
        {
            var tickDuration = UnityEngine.Random.Range(0.8f, 1.2f);
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
            transform.position += Stat.MoveSpeed * Time.deltaTime * direction;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CoProcessContact()
    {
        while (_isContacting)
        {
            _target.Stat.TakeDamage((int)Stat.AttackDamage);
            Debug.Log($"{gameObject.name} attacked Player. HP Left : {_target.Stat.CurrentHP}");
            var delay = AttackDelay / Stat.AttackSpeed;
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
