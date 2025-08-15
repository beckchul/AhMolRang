
public abstract class SkillBase
{
    public int SkillId { get; }
    public SkillType SkillType { get; }

    public int Level { get; set; }
    public float ATKCoefficient { get; set; } // 공격력 계수 (공격력과 곱해서 위력 결정)

    public SkillBase(int skillId, SkillType skillType, int level, int atkCoefficient)
    {
        SkillId = skillId;
        SkillType = skillType;
        Level = level;
        ATKCoefficient = atkCoefficient;
    }

    public abstract void Use();
}