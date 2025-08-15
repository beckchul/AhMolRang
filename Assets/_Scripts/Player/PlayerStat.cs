using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MaxHP = 0,
    RegenHP,
    AttackDamage,
    ProjectileDamage,
    ProjectileNum,
    ProjectileSpeed,
    AreaDamage,
    AreaBoundary,
    DotDamage,
    DotDuration,
    MoveSpeed,
    FinishPercent,
    CoolTime,
    CriticalPer,
    CriticalDamage,
    EXPArea,
    EXPUp,
    Defense,
    GoldUp
}

public class PlayerStat : CharacterStat
{
    #region << =========== STAT =========== >>

    public CharacterStat Stat { get; private set; }

    //public Stat MaxHPStat { get; private set; }
    public Stat RegenHPStat { get; private set; }
    public Stat AttackDamageStat { get; private set; }
    public Stat ProjectileDamageStat { get; private set; }
    public Stat ProjectileNumStat { get; private set; }
    public Stat ProjectileSpeedStat { get; private set; }
    public Stat AreaDamageStat { get; private set; }
    public Stat AreaBoundaryStat { get; private set; }
    public Stat DotDamageStat { get; private set; }
    public Stat DotDurationStat { get; private set; }
    //public Stat MoveSpeedStat { get; private set; }
    public Stat FinishPercentStat { get; private set; }
    public Stat CoolTimeStat { get; private set; }
    public Stat CriticalPerStat { get; private set; }
    public Stat CriticalDamageStat { get; private set; }
    public Stat EXPAreaStat { get; private set; }
    public Stat EXPUpStat { get; private set; }
    public Stat DefenseStat { get; private set; }
    public Stat GoldUpStat { get; private set; }


    //public int MaxHP => (int)MaxHPStat.FinalValue;
    public float RegenHP => RegenHPStat.FinalValue;
    public float AttackDamage => AttackDamageStat.FinalValue;
    public float ProjectileDamage => ProjectileDamageStat.FinalValue;
    public float ProjectileNum => ProjectileNumStat.FinalValue;
    public float ProjectileSpeed => ProjectileSpeedStat.FinalValue;
    public float AreaDamage => AreaDamageStat.FinalValue;
    public float AreaBoundary => AreaBoundaryStat.FinalValue;
    public float DotDamage => DotDamageStat.FinalValue;
    public float DotDuration => DotDurationStat.FinalValue;
    //public float MoveSpeed => MoveSpeedStat.FinalValue;
    public float FinishPercent => FinishPercentStat.FinalValue;
    public float CoolTime => CoolTimeStat.FinalValue;
    public float CriticalPer => CriticalPerStat.FinalValue;
    public float CriticalDamage => CriticalDamageStat.FinalValue;
    public float EXPArea => EXPAreaStat.FinalValue;
    public float EXPUp => EXPUpStat.FinalValue;
    public float Defense => DefenseStat.FinalValue;
    public float GoldUp => GoldUpStat.FinalValue;

    //public int CurrentHP { get; private set; }

    #endregion

    #region << =========== DELEGATE =========== >>

    public event Action<int> OnTakeDamage;
    public event Action OnDeath;

    #endregion

    #region << =========== DICTINARY =========== >>

    private readonly Dictionary<StatType, Stat> Dict = new();


    #endregion


    public void InitPlayerStat()
    {
        Stat = new CharacterStat();

        //MaxHPStat = new Stat((float)Stat.hp);
        //CurrentHP = MaxHP;
        //MoveSpeedStat = new Stat(Stat.moveSpeed);

        CoolTimeStat = new Stat(1.0f);
    }

    public int TakeDamage(int damge)
    {
        OnTakeDamage?.Invoke(damge);

        //CurrentHP = CurrentHP - damge <= 0 ? 0 : CurrentHP - damge;
        //if (CurrentHP <= 0) Death();

        return CurrentHP;
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }

    public int AddMaxHP(int addValue)
    {
        StatModifier md = new StatModifier(addValue, StatModifierType.Flat);
        MaxHPStat.AddModifier(md);
        //버프 오브젝트 저장해야함

        return (int)MaxHPStat.FinalValue;
    }

    public float AddMoveSpeed(float addValue)
    {
        StatModifier md = new StatModifier(addValue, StatModifierType.Flat);
        MoveSpeedStat.AddModifier(md);
        //버프 오브젝트 저장해야함

        return MoveSpeedStat.FinalValue;
    }
}
