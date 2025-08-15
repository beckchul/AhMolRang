using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _themeObjects;
    private float attackDelay = 1f;
    public CharacterStat Stat { get; private set; }

    private Collider2D _collider;
    private bool _isContacting;

    public void Set(Action<MonsterBase> onDead)
    {
        Stat = new CharacterStat();
        StartCoroutine(CoTick());
        _collider = GetComponent<Collider2D>();
        _isContacting = false;
        Stat.OnDeath = () => onDead?.Invoke(this);

        // Set the theme objects based on the theme type
        var themeType = SkillManager.Instance.GetRandomTheme();
        for (int i = 0; i < _themeObjects.Count; i++)
        {
            if ((ThemeType)i == themeType)
            {
                _themeObjects[i].SetActive(true);
            }
            else
            {
                _themeObjects[i].SetActive(false);
            }
        }
    }

    private IEnumerator CoTick()
    {
        while (Stat.CurrentHP > 0)
        {
            var tickDuration = UnityEngine.Random.Range(0.8f, 1.2f);
            if (PlayerManager.Instance.PlayerScript != null)
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
            var direction = (PlayerManager.Instance.PlayerScript.transform.position - transform.position).normalized;
            transform.position += Stat.MoveSpeed * Time.deltaTime * direction;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CoProcessContact()
    {
        while (_isContacting)
        {
            var target = PlayerManager.Instance.PlayerScript;
            target.Stat.TakeDamage((int)Stat.AttackDamage);
            Debug.Log($"{gameObject.name} attacked Player. HP Left : {target.Stat.CurrentHP}");
            var delay = attackDelay / Stat.AttackSpeed;
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
