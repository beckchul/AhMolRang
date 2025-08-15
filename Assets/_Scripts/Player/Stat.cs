using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Stat
{
    private readonly List<StatModifier> modifiers = new();

    public Stat(float baseValue)
    {
        BaseValue = baseValue;
    }

    public float BaseValue { get; set; }
    public float FinalValue => CalculateFinalValue();

    public void AddModifier(StatModifier modifier) => modifiers.Add(modifier);
    public void RemoveModifier(StatModifier modifier) => modifiers.Remove(modifier);
    public void ClearModifier() => modifiers.Clear();
    public List<StatModifier> GetModifierList() => modifiers;

    private float CalculateFinalValue()
    {
        float flatSum = 0f;
        float percentMul = 1f;

        StatModifier constMod = modifiers.Find(m => m.Type == StatModifierType.Const);
        if (constMod != null) return constMod.Value;

        foreach (var mod in modifiers)
        {
            switch (mod.Type)
            {
                case StatModifierType.Flat:
                    flatSum += mod.Value;
                    break;
                case StatModifierType.Percent:
                    percentMul *= 1 + (mod.Value / 100f);
                    break;
            }
        }

        return (BaseValue + flatSum) * percentMul;
    }

    //public string GetCalculationString()
    //{
    //    StatModifier constMod = modifiers.Find(m => m.Type == StatType.Const);
    //    if (constMod != null) return $"{BaseValue} const = {constMod.Value}";

    //    StringBuilder sb = new StringBuilder();
    //    sb.Append(BaseValue);

    //    foreach (var mod in modifiers)
    //    {
    //        if (mod.Type == StatType.Flat)
    //            sb.Append($" + {mod.Value}");
    //    }
    //    foreach (var mod in modifiers)
    //    {
    //        if (mod.Type == StatType.Percent)
    //            sb.Append($" ¡¿ (1 + {mod.Value / 100} )");
    //    }

    //    sb.Append($" = {FinalValue}");

    //    return sb.ToString();
    //}
}
