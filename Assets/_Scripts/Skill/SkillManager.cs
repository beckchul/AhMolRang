using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    public SkillDataScriptableObject skillData;

    public Dictionary<int, SkillStatus> Skills = new();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (var skillData in skillData.activeSkills)
        {
            var skill = new ActiveSkillStatus(
                skillData.skillId, skillData.efficiency, skillData.cooldown, skillData.skillPrefab);
            Skills.Add(skillData.skillId, skill);
        }

        foreach (var skillData in skillData.passiveSkills)
        {
            var skill = new SkillStatus(skillData.skillId, skillData.efficiency);
            Skills.Add(skillData.skillId, skill);
        }
    }

    public ActiveSkill GetSkillObject(int skillId)
    {
        if (Skills.TryGetValue(skillId, out var skillStatus))
        {
            if (skillStatus is not ActiveSkillStatus activeSkillStatus)
            {
                Debug.LogWarning($"Skill with ID {skillId} is not an active skill.");
                return null;
            }
            else
            {
                var skill = Instantiate(activeSkillStatus.SkillPrefab);
                return skill;
            }
        }

        return null;
    }
}
