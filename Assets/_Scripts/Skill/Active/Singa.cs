using System.Collections;
using UnityEngine;

public class Singa : ActiveSkill
{
    [SerializeField]
    private CircleCollider2D _collider;
    [SerializeField]
    private Animator _anim;

    private bool firstStart = false;

    private void Awake()
    {
    }

    public override void StartLifeCycle()
    {
        base.StartLifeCycle();
        StartCoroutine(CoProcessEffect());

        firstStart = true;
    }

    public IEnumerator CoProcessEffect()
    {
        while (gameObject.activeSelf)
        {
            PlaySound1(0.8f);
            _anim.Play("Singa");

            var colliders = Physics2D.OverlapCircleAll(transform.position, _collider.radius, LayerMask.GetMask("Enemy"));
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out MonsterBase monster))
                {
                    var stat = monster.Stat;
                    //int damage = Mathf.RoundToInt(stat.AttackDamage * Efficiency);
                    stat.TakeDamage(200);
                }
            }

            yield return new WaitForSeconds(Cooldown);
        }
    }
}
