public class ActiveSkillStatus : SkillStatus
{
    public ActiveSkill SkillPrefab { get; }

    public ActiveSkillStatus(int skillId, ActiveSkill skillPrefab) : base(skillId)
    {
        SkillPrefab = skillPrefab;
    }
}