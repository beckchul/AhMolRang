using UnityEngine;

public enum StatModifierType
{
    Flat = 0,
    Percent = 1,
    Const = 2
}

public class StatModifier
{
    public float Value { get; }
    public StatModifierType Type { get; }

    public StatModifier(float value, StatModifierType type)
    {
        Value = value;
        Type = type;
    }
}
