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
    private Dictionary<int, ObjectPool<ActiveSkill>> _skillPools = new();

    private Dictionary<int, ActiveSkillData> _skillDataCache = new();

    public override void Init()
    {
        foreach (var skillData in skillData.activeSkills)
        {
            var skill = new ActiveSkillStatus(
                skillData.skillId, skillData.efficiency, skillData.cooldown, skillData.skillPrefab);
            Skills.Add(skillData.skillId, skill);
            _skillDataCache.Add(skillData.skillId, skillData);

            var skillPool = new ObjectPool<ActiveSkill>(
                () => OnCreateSkill(skillData),
                OnGetSkill,
                skill => skill.gameObject.SetActive(false),
                skill => Destroy(skill.gameObject),
                false,
                10,
                50
            );
        }

        foreach (var skillData in skillData.passiveSkills)
        {
            var skill = new SkillStatus(skillData.skillId, skillData.efficiency);
            Skills.Add(skillData.skillId, skill);
        }
    }

    public ActiveSkill GetSkill(int skillId)
    {
        if (!_skillPools.TryGetValue(skillId, out var pool))
        {
            return null;
        }

        return pool.Get();
    }

    private ActiveSkill OnCreateSkill(ActiveSkillData skillData)
    {
        var skill = Instantiate(skillData.skillPrefab);
        return skill;
    }

    private void OnGetSkill(ActiveSkill skill)
    {
        skill.gameObject.SetActive(true);

        var activeSkillStatus = Skills[skill.SkillId] as ActiveSkillStatus;
        skill.Init(
            activeSkillStatus.SkillId,
            activeSkillStatus.Level,
            activeSkillStatus.Efficiency,
            activeSkillStatus.Cooldown);
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
        if (skillStatus is ActiveSkillStatus activeStatus &&
            activeStatus.Cooldown == 0)
        {
            if (_skillPools.TryGetValue(skillId, out var pool))
            {
                var skill = GetSkill(skillId);
                skill.transform.SetParent(PlayerManager.Instance.PlayerObject.transform);
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
