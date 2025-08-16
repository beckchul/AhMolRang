using System;
using System.Collections;
using UnityEngine;

public class MagicClawEffect : MonoBehaviour
{
    private int damage;
    private MonsterBase monster;

    private Action<MagicClawEffect> onHit;
    private Action<MagicClawEffect> onExpired;

    public void Shoot(int damage, MonsterBase monster, Action<MagicClawEffect> onHit, Action<MagicClawEffect> onExpired)
    {
        this.damage = damage;
        this.onHit = onHit;
        this.onExpired = onExpired;
        this.monster = monster;

        gameObject.SetActive(true);
    }

    public void DamageEvent()
    {
        int value = (int)(damage / 2 + 0.5f);
        monster.Stat.TakeDamage(value);
        onHit?.Invoke(this);
    }

    public void EndEvent()
    {
        onExpired?.Invoke(this);
    }
}
