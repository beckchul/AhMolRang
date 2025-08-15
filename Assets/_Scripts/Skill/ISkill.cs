using UnityEngine;

public interface ISkill
{
    public int SkillId { get; }
    public SkillType SkillType { get; }
    public int Level { get; }
    public float Efficiency { get; } // 계수 (공격력과 곱해서 위력 결정, 버프일 경우 스탯에 곱해지는 값)

    public void Upgrade();
}
