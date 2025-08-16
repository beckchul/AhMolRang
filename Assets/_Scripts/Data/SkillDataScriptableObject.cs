using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillDataScriptableObject : ScriptableObject
{
    [Serializable]
    public class SkillDataBase
    {
        public int skillId;
        public string skillName;
        public Sprite skillIcon;
        public SkillType skillType;
        public int skillLevel;
    }

    [Serializable]
    public class ActiveSkillData : SkillDataBase
    {
        public ActiveSkill skillPrefab;
        public ThemeType themeType;
        public int SoundID;
    }

    [Serializable]
    public class PassiveSkillData : SkillDataBase
    {
    }

    public List<ActiveSkillData> activeSkills;
    public List<PassiveSkillData> passiveSkills;
}