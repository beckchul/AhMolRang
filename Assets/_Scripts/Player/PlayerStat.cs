using System;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    #region << =========== FINAL VALUE =========== >>

    public CharacterStat Stat { get; private set; }

    public Stat MaxHPStat { get; private set; }
    public Stat MoveSpeedStat { get; private set; }

    public int MaxHP => (int)MaxHPStat.FinalValue;
    public float MoveSpeed => MoveSpeedStat.FinalValue;

    public int CurrentHP { get; private set; }

    #endregion

    #region << =========== DELEGATE =========== >>

    public event Action<int> OnTakeDamage;
    public event Action OnDeath;

    #endregion


    public void InitPlayerStat()
    {
        Stat = new CharacterStat();

        MaxHPStat = new Stat((float)Stat.hp);
        CurrentHP = MaxHP;
        MoveSpeedStat = new Stat(Stat.moveSpeed);
    }

    public int TakeDamage(int damge)
    {
        OnTakeDamage?.Invoke(damge);

        CurrentHP = CurrentHP - damge <= 0 ? 0 : CurrentHP - damge;
        if (CurrentHP <= 0) Death();

        return CurrentHP;
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }

    public int ChangeMaxHP(int flatValue)
    {
        flatValue = flatValue <= 0 ? 1 : flatValue;

        StatModifier md = new StatModifier(flatValue, StatType.Flat);
        MaxHPStat.AddModifier(md);

        return (int)MaxHPStat.FinalValue;
    }

    public float ChangeMoveSpeed(float flatValue)
    {
        flatValue = flatValue <= 0.1f ? 0.1f : flatValue;

        StatModifier md = new StatModifier(flatValue, StatType.Flat);
        MaxHPStat.AddModifier(md);

        return MoveSpeedStat.FinalValue;
    }
}
