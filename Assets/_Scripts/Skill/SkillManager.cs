using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SkillDataScriptableObject;

public class SkillManager : MonoSingleton<SkillManager>
{
    public readonly int MaxActiveSkillCount = 6;

    public readonly int MaxPasiveSkillCount = 6;

    public SkillDataScriptableObject skillData;

    public Dictionary<int, SkillStatus> Skills = new();
    private Dictionary<int, ActiveSkill> _skillObjects = new();

    private Dictionary<int, ActiveSkillData> _activeSkillDataCache = new();
    private Dictionary<int, PassiveSkillData> _passiveSkillDataCache = new();

    public override void Init()
    {
        foreach (var skillData in skillData.activeSkills)
        {
            var skill = new ActiveSkillStatus(
                skillData.skillId, skillData.skillPrefab);
            Skills.Add(skillData.skillId, skill);
            _activeSkillDataCache.Add(skillData.skillId, skillData);

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
            var skill = new SkillStatus(skillData.skillId);
            Skills.Add(skillData.skillId, skill);
            _passiveSkillDataCache.Add(skillData.skillId, skillData);
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
        return _activeSkillDataCache[skllId].themeType;
    }

    public void SkillLevelUp(int skillId)
    {
        var skillStatus = Skills[skillId];
        skillStatus.LevelUp();

        if (skillStatus is ActiveSkillStatus activeSkillStatus)
        {
            var skill = _skillObjects[skillId];
            if (activeSkillStatus.Level == 1)
            {
                skill.Init(
                    skillId,
                    activeSkillStatus.Level);
                skill.StartLifeCycle();

                foreach (var activeSkilldata in skillData.activeSkills)
                {
                    if(activeSkilldata.skillId == skillId) 
                    {
                        SoundManager.Instance.PlaySFX(activeSkilldata.SoundID);
                        break;
                    }
                }
            }
            else
            {
                skill.Upgrade();
            }
        }
    }

    public ActiveSkillStatus GetActiveSkillStatus(int skillId)
    {
        return Skills[skillId] as ActiveSkillStatus;
    }

    public SkillStatus GetPasiveSkillStatus(int skillId)
    {
        foreach (var skill in skillData.passiveSkills)
        {
            return Skills[skillId];
        }

        return null;
    }

    public SkillDataBase GetSkillData(int skillId)
    {
        if (_activeSkillDataCache.TryGetValue(skillId, out var activeSkillData))
        {
            return activeSkillData;
        }
        else if (_passiveSkillDataCache.TryGetValue(skillId, out var passiveSkillData))
        {
            return passiveSkillData;
        }
        Debug.LogWarning($"Skill data for skill ID {skillId} not found.");
        return null;
    }
}
