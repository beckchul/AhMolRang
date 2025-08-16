using System;
using UnityEngine;

public class CharacterStat
{

    #region << =========== DELEGATE =========== >>
    public Action<int> OnTakeDamage;
    public Action OnDeath;
    #endregion

    public Stat MaxHPStat { get; private set; }
    public Stat MoveSpeedStat { get; private set; }
    public Stat AttackSpeedStat { get; private set; }
    public Stat AttackDamageStat { get; private set; }

    public int MaxHP => (int)MaxHPStat.FinalValue;
    public float MoveSpeed => MoveSpeedStat.FinalValue;
    public float AttackSpeed => AttackSpeedStat.FinalValue;
    public float AttackDamage => AttackDamageStat.FinalValue;

    public int CurrentHP { get; private set; }

    public CharacterStat(
        int hp = 100,
        float moveSpeed = 1f,
        float attackSpeed = 1f,
        float attackDamage = 1f
        )
    {
        MaxHPStat = new Stat((float)hp);
        MoveSpeedStat = new Stat(moveSpeed);
        AttackSpeedStat = new Stat(attackSpeed);
        AttackDamageStat = new Stat(attackDamage);

        CurrentHP = (int)MaxHPStat.BaseValue;

        Reset();
    }

    public void Reset()
    {
        MaxHPStat.ClearModifier();
        MoveSpeedStat.ClearModifier();
        AttackSpeedStat.ClearModifier();
        AttackDamageStat.ClearModifier();

        CurrentHP = (int)MaxHPStat.BaseValue;
    }

    public virtual void TakeDamage(int damage)
    {
        if (CurrentHP <= 0) return;

        OnTakeDamage?.Invoke(damage);

        CurrentHP -= damage;

        if (CurrentHP <= 0) Death();
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }

    public void AddMaxHP(int addValue)
    {
        StatModifier md = new StatModifier(addValue, StatModifierType.Flat);
        MaxHPStat.AddModifier(md);
    }

    public void AddMoveSpeed(float addValue)
    {
        StatModifier md = new StatModifier(addValue, StatModifierType.Flat);
        MoveSpeedStat.AddModifier(md);
    }

    public void AddAttackSpeed(float addValue)
    {
        StatModifier md = new StatModifier(addValue, StatModifierType.Flat);
        AttackSpeedStat.AddModifier(md);
    }

    public void AddAttackDamage(float addValue)
    {
        StatModifier md = new StatModifier(addValue, StatModifierType.Flat);
        AttackDamageStat.AddModifier(md);
    }

    public void MultiMaxHP(float mulValue)
    {
        StatModifier md = new StatModifier(mulValue, StatModifierType.Flat);
        MaxHPStat.AddModifier(md);
    }

    public void MultiMoveSpeed(float mulValue)
    {
        StatModifier md = new StatModifier(mulValue, StatModifierType.Flat);
        MoveSpeedStat.AddModifier(md);
    }

    public void MultiAttackSpeed(float mulValue)
    {
        StatModifier md = new StatModifier(mulValue, StatModifierType.Flat);
        AttackSpeedStat.AddModifier(md);
    }

    public void MultiAttackDamage(float mulValue)
    {
        StatModifier md = new StatModifier(mulValue, StatModifierType.Flat);
        AttackDamageStat.AddModifier(md);
    }
}
