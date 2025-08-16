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
    public Stat RegenHPStat { get; private set; }
    public Stat ProjectileDamageStat { get; private set; }
    public Stat ProjectileNumStat { get; private set; }
    public Stat ProjectileSpeedStat { get; private set; }
    public Stat AreaDamageStat { get; private set; }
    public Stat AreaBoundaryStat { get; private set; }
    public Stat DotDamageStat { get; private set; }
    public Stat DotDurationStat { get; private set; }
    public Stat FinishPercentStat { get; private set; }
    public Stat CoolTimeStat { get; private set; }
    public Stat CriticalPerStat { get; private set; }
    public Stat CriticalDamageStat { get; private set; }
    public Stat EXPAreaStat { get; private set; }
    public Stat EXPUpStat { get; private set; }
    public Stat DefenseStat { get; private set; }
    public Stat GoldUpStat { get; private set; }

    public float RegenHP => RegenHPStat.FinalValue;
    public float ProjectileDamage => ProjectileDamageStat.FinalValue;
    public int ProjectileNum => (int)ProjectileNumStat.FinalValue;
    public float ProjectileSpeed => ProjectileSpeedStat.FinalValue;
    public float AreaDamage => AreaDamageStat.FinalValue;
    public float AreaBoundary => AreaBoundaryStat.FinalValue;
    public float DotDamage => DotDamageStat.FinalValue;
    public float DotDuration => DotDurationStat.FinalValue;
    public float FinishPercent => FinishPercentStat.FinalValue;
    public float CoolTime => CoolTimeStat.FinalValue;
    public float CriticalPer => CriticalPerStat.FinalValue;
    public float CriticalDamage => CriticalDamageStat.FinalValue;
    public float EXPArea => EXPAreaStat.FinalValue;
    public float EXPUp => EXPUpStat.FinalValue;
    public float Defense => DefenseStat.FinalValue;
    public float GoldUp => GoldUpStat.FinalValue;

    public int MaxEXP { get; private set; }
    public int CurrentEXP { get; private set; }
    public int MaxLevel { get; private set; }
    public int CurrentLevel { get; private set; }
    #endregion

    #region << =========== DICTINARY =========== >>
    private readonly Dictionary<StatType, Stat> statDict = new();
    #endregion

    #region << =========== STAT =========== >>
    public event Action OnLevelUp;
    #endregion


    public PlayerStat() : base(100, 2)
    {
        InitPlayerStat();
    }

    public void InitPlayerStat()
    {
        RegenHPStat = new Stat(0.0f);
        ProjectileDamageStat = new Stat(0.0f);
        ProjectileNumStat = new Stat(0.0f);
        ProjectileSpeedStat = new Stat(1.0f);
        AreaDamageStat = new Stat(0.0f);
        AreaBoundaryStat = new Stat(0.0f);
        DotDamageStat = new Stat(0.0f);
        DotDurationStat = new Stat(0.0f);
        FinishPercentStat = new Stat(0.0f);
        CoolTimeStat = new Stat(0.0f);
        CriticalPerStat = new Stat(0.0f);
        CriticalDamageStat = new Stat(0.0f);
        EXPAreaStat = new Stat(5.0f);
        EXPUpStat = new Stat(0.0f);
        DefenseStat = new Stat(0.0f);
        GoldUpStat = new Stat(0.0f);

        MaxEXP = 10;
        CurrentEXP = 0;
        MaxLevel = 100;
        CurrentLevel = 1;

        statDict.Add(StatType.RegenHP, RegenHPStat);
        statDict.Add(StatType.ProjectileDamage, ProjectileDamageStat);
        statDict.Add(StatType.ProjectileNum, ProjectileNumStat);
        statDict.Add(StatType.ProjectileSpeed, ProjectileSpeedStat);
        statDict.Add(StatType.AreaDamage, AreaDamageStat);
        statDict.Add(StatType.AreaBoundary, AreaBoundaryStat);
        statDict.Add(StatType.DotDamage, DotDamageStat);
        statDict.Add(StatType.DotDuration, DotDurationStat);
        statDict.Add(StatType.FinishPercent, FinishPercentStat);
        statDict.Add(StatType.CoolTime, CoolTimeStat);
        statDict.Add(StatType.CriticalPer, CriticalPerStat);
        statDict.Add(StatType.CriticalDamage, CriticalDamageStat);
        statDict.Add(StatType.EXPArea, EXPAreaStat);
        statDict.Add(StatType.EXPUp, EXPUpStat);
        statDict.Add(StatType.Defense, DefenseStat);
        statDict.Add(StatType.GoldUp, GoldUpStat);
    }

    public void ChangeStat(StatModifierType statModifierType, StatType statType, float value)
    {
        if(statDict.ContainsKey(statType))
        {
            StatModifier md = new StatModifier(value, statModifierType);
            statDict[statType].AddModifier(md);
        }
        else
        {
            Debug.LogError("PlayerStat -> ChangeStat: statDict.ContainsKey Error");
        }
    }

    public void AddEXP(int add)
    {
        CurrentEXP += add;

        if(CurrentEXP >= MaxEXP)
        {
            if(CurrentLevel++ <= MaxLevel) LevelUp();
            else CurrentEXP = MaxEXP;
        }

        UIManager.Instance.UpdateExp();
    }

    public void LevelUp()
    {
        CurrentEXP = CurrentEXP - MaxEXP;
        OnLevelUp?.Invoke();
    }
}
