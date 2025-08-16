public class SkillStatus
{
    public int SkillId { get; }
    public float Efficiency { get; protected set; }
    public int Level { get; set; }

    public SkillStatus(int skillId, float efficiency)
    {
        SkillId = skillId;
        Efficiency = efficiency;
        Level = 0;
    }

    public void UpgradeEfficiency(float efficiencyBonusRate)
    {
        Level++;
        Efficiency *= efficiencyBonusRate;
    }
}