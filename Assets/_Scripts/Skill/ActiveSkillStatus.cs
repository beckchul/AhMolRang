public class ActiveSkillStatus : SkillStatus
{
    public ActiveSkill SkillPrefab { get; }
    public float Cooldown { get; private set; }

    public ActiveSkillStatus(int skillId, float efficiency, float cooldown, ActiveSkill skillPrefab) : base(skillId, efficiency)
    {
        Cooldown = cooldown;
        SkillPrefab = skillPrefab;
    }
}