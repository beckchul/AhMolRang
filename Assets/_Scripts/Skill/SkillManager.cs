using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;
using static SkillDataScriptableObject;

public class SkillManager : MonoSingleton<SkillManager>
{
    public SkillDataScriptableObject skillData;

    public Dictionary<int, SkillStatus> Skills = new();
    private Dictionary<int, ActiveSkill> _skillObjects = new();

    private Dictionary<int, ActiveSkillData> _skillDataCache = new();

    public override void Init()
    {
        foreach (var skillData in skillData.activeSkills)
        {
            var skill = new ActiveSkillStatus(
                skillData.skillId, skillData.efficiency, skillData.cooldown, skillData.skillPrefab);
            Skills.Add(skillData.skillId, skill);
            _skillDataCache.Add(skillData.skillId, skillData);

            if (skillData.skillPrefab)
            {
                var skillObject = Instantiate(skillData.skillPrefab, PlayerManager.Instance.PlayerObject.transform);
                _skillObjects.Add(skillData.skillId, skillObject);
            }
            else
            {
                Debug.LogWarning($"Skill prefab for skill ID {skillData.skillId} is not assigned.");
            }
        }

        foreach (var skillData in skillData.passiveSkills)
        {
            var skill = new SkillStatus(skillData.skillId, skillData.efficiency);
            Skills.Add(skillData.skillId, skill);
        }
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

    public void SkillLevelUp(int skillId)
    {
        var skillStatus = Skills[skillId];
        skillStatus.UpgradeEfficiency(1);

        if (skillStatus is ActiveSkillStatus activeSkillStatus)
        {
            if (activeSkillStatus.Level == 1)
            {
                var skill = _skillObjects[skillId];
                skill.Init(
                    skillId,
                    activeSkillStatus.Level,
                    activeSkillStatus.Efficiency,
                    activeSkillStatus.Cooldown);
                skill.StartLifeCycle();
            }
        }
    }

    public ActiveSkillData GetActiveSkillData(int skillId)
    {
        foreach (var skill in skillData.activeSkills)
        {
            if (skill.skillId == skillId)
            {
                skill.skillLevel = Skills[skillId].Level;
                return skill;
            }
        }

        return null;
    }

    public PassiveSkillData GetPasiveSkillData(int skillId)
    {
        foreach (var skill in skillData.passiveSkills)
        {
            if (skill.skillId == skillId)
            {
                skill.skillLevel = Skills[skillId].Level;
                return skill;
            }
        }

        return null;
    }
}
