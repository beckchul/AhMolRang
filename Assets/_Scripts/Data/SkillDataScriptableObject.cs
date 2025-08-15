using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillDataScriptableObject : ScriptableObject
{
    [Serializable]
    public struct ActiveSkillData
    {
        public int skillId;
        public float efficiency;
        public float cooldown;
        public ActiveSkill skillPrefab;
        public string skillName;
        public Sprite skillIcon;
        public ThemeType themeType;
    }

    [Serializable]
    public struct PassiveSkillData
    {
        public int skillId;
        public float efficiency;
        public string skillName;
        public Sprite skillIcon;
    }

    public List<ActiveSkillData> activeSkills;
    public List<PassiveSkillData> passiveSkills;
}