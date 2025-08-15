using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    public SkillDataScriptableObject skillData;

    public Dictionary<int, SkillStatus> Skills = new();

    private Dictionary<int, SkillDataScriptableObject.ActiveSkillData> _skillDataCache = new();

    public override void Init()
    {
        foreach (var skillData in skillData.activeSkills)
        {
            var skill = new ActiveSkillStatus(
                skillData.skillId, skillData.efficiency, skillData.cooldown, skillData.skillPrefab);
            Skills.Add(skillData.skillId, skill);
            _skillDataCache.Add(skillData.skillId, skillData);
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

    public ThemeType GetRandomTheme()
    {
        var themes = Skills.Values
            .Where(x => x.Level > 0 && x is ActiveSkillStatus)
            .Select(x => x.SkillId);
        var randomIndex = Random.Range(0, themes.Count());
        if (themes.Count() == 0)
        {
            return default;
        }

        var skllId = themes.ElementAt(randomIndex);
        return _skillDataCache[skllId].themeType;
    }
}
