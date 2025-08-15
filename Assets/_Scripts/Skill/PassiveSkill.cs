using UnityEngine;

public class PassiveSkill : ISkill
{
    private int skillId;
    private int level;
    private float efficiency;
    
    public int SkillId => skillId;
    public float Efficiency => efficiency;
    public int Level => level;

    public SkillType SkillType => SkillType.Passive;

    public PassiveSkill(
        int skillId,
        float efficiency)
    {
        this.skillId = skillId;
        this.efficiency = efficiency;
        level = 0;
    }

    public void Upgrade()
    {
    }
}