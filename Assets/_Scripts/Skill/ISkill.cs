using UnityEngine;

public interface ISkill
{
    public int SkillId { get; }
    public SkillType SkillType { get; }
    public int Level { get; }

    public void Upgrade();
}
