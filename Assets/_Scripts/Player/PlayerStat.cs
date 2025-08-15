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
    #endregion

    #region << =========== DICTINARY =========== >>
    private readonly Dictionary<StatType, Stat> Dict = new();
    #endregion


    public PlayerStat() : base()
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
        EXPAreaStat = new Stat(0.0f);
        EXPUpStat = new Stat(0.0f);
        DefenseStat = new Stat(0.0f);
        GoldUpStat = new Stat(0.0f);
    }


}
