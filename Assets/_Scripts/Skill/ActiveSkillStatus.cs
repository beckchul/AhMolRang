public class ActiveSkillStatus : SkillStatus
{
    public ActiveSkill SkillPrefab { get; }
    public float Cooldown { get; protected set; }

    public ActiveSkillStatus(int skillId, float cooldown, ActiveSkill skillPrefab) : base(skillId)
    {
        Cooldown = cooldown;
        SkillPrefab = skillPrefab;
    }

    public void UpgradeCooldown(float cooldownReductionRate)
    {
        Level++;
        Cooldown *= (1 - cooldownReductionRate);
    }
}