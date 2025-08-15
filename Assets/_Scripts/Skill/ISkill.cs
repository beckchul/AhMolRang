using UnityEngine;

public interface ISkill
{
    public int SkillId { get; }
    public SkillType SkillType { get; }
    public int Level { get; }
    public float Efficiency { get; } // ��� (���ݷ°� ���ؼ� ���� ����, ������ ��� ���ȿ� �������� ��)

    public void Upgrade();
}
