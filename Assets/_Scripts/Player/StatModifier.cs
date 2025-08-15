using UnityEngine;

public enum StatType
{
    Flat = 0,
    Percent = 1,
    Const = 2
}

public class StatModifier
{
    public float Value { get; }
    public StatType Type { get; }

    public StatModifier(float value, StatType type)
    {
        Value = value;
        Type = type;
    }
}
