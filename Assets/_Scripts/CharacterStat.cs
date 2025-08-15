using UnityEngine;

public class CharacterStat
{
    public Stat MaxHPStat { get; private set; }
    public Stat MoveSpeedStat { get; private set; }
    public Stat AttackSpeedStat { get; private set; }

    public int MaxHP => (int)MaxHPStat.FinalValue;
    public float MoveSpeed => MoveSpeedStat.FinalValue;
    public float AttackSpeed => AttackSpeedStat.FinalValue;

    public int CurrentHP { get; private set; }



    public CharacterStat(
        int hp = 100,
        float moveSpeed = 1f,
        float attackSpeed = 1f)
    {
        MaxHPStat = new Stat((float)hp);
        MoveSpeedStat = new Stat(moveSpeed);
        AttackSpeedStat = new Stat(attackSpeed);

        CurrentHP = (int)MaxHPStat.BaseValue;

        Reset();
    }

    public void Reset()
    {
        MaxHPStat.ClearModifier();
        MoveSpeedStat.ClearModifier();
        AttackSpeedStat.ClearModifier();

        CurrentHP = (int)MaxHPStat.BaseValue;
    }
}
