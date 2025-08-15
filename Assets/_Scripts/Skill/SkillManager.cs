using System.Collections.Generic;

public class SkillManager : MonoSingleton<SkillManager>
{


    private Dictionary<int, SkillBase> _skills = new();
}