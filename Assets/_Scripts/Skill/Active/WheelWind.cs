using System.Collections;
using UnityEngine;

public class WheelWind : ActiveSkill
{
    [SerializeField]
    private CircleCollider2D _collider;
    [SerializeField]
    private SpriteRenderer _renderer;

    private bool firstStart = false;

    private void Awake()
    {
        _renderer.enabled = false;
    }

    private void Update()
    {
        if(Time.timeScale <= 0.1f) StopSound(sound_ID_1);
        else
        {
            if(firstStart && !PlayingSound) PlaySound1(0.8f);
        }
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        _renderer.enabled = true;
        StartCoroutine(CoProcessEffect());

        PlaySound1(0.8f);
        firstStart = true;
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _collider.radius, LayerMask.GetMask("Enemy"));
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out MonsterBase monster))
                {
                    var stat = monster.Stat;
                    //int damage = Mathf.RoundToInt(stat.AttackDamage * Efficiency);
                    stat.TakeDamage(100);
                }
            }

            yield return new WaitForSeconds(Cooldown);
        }
    }
}
