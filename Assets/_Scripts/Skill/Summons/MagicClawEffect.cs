using System;
using System.Collections;
using UnityEngine;

public class MagicClawEffect : MonoBehaviour
{
    private Action<MagicClawEffect> onHit;
    private Action<MagicClawEffect> onExpired;

    private MonsterBase monster;

    public void Shoot(MonsterBase monster, Action<MagicClawEffect> onHit, Action<MagicClawEffect> onExpired)
    {
        this.monster = monster;
        this.onHit = onHit;
        this.onExpired = onExpired;

        gameObject.SetActive(true);
    }

    public void DamageEvent()
    {
        onHit?.Invoke(this);
    }

    public void EndEvent()
    {
        onExpired?.Invoke(this);
    }
}
