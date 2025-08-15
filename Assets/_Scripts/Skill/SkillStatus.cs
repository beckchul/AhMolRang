public class SkillStatus
{
    public int SkillId { get; }
    public float Efficiency { get; protected set; }
    public int Level { get; protected set; }

    public SkillStatus(int skillId, float efficiency)
    {
        SkillId = skillId;
        Efficiency = efficiency;
        Level = 1;
    }


    public void UpgradeEfficiency(float efficiencyBonusRate)
    {
        Level++;
        Efficiency *= efficiencyBonusRate;
    }
}