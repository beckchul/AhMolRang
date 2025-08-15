
public abstract class SkillBase
{
    public int SkillId { get; }
    public SkillType SkillType { get; }

    public int Level { get; set; }
    public float ATKCoefficient { get; set; } // ���ݷ� ��� (���ݷ°� ���ؼ� ���� ����)

    public SkillBase(int skillId, SkillType skillType, int level, int atkCoefficient)
    {
        SkillId = skillId;
        SkillType = skillType;
        Level = level;
        ATKCoefficient = atkCoefficient;
    }

    public abstract void Use();
}