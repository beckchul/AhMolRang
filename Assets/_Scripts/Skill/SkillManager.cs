using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    public SkillDataScriptableObject skillData;

    private Dictionary<int, SkillBase> _skills = new();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {

    }
}