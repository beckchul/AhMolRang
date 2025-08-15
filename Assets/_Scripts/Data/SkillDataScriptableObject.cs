using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
public class SkillDataScriptableObject : ScriptableObject
{
    [Serializable]
    public struct SkillData
    {
        public int skillId;
        public SkillType skillType;
        public GameObject skillPrefab;
        public string skillName;
    }

    public List<SkillData> skills;
}