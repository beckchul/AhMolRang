public class SkillStatus
{
    public int SkillId { get; }
    public float Efficiency { get; private set; }
    public int Level { get; private set; }

    public SkillStatus(int skillId, float efficiency)
    {
        SkillId = skillId;
        Efficiency = efficiency;
        Level = 0;
    }


    public void Upgrade()
    {
        Level++;
        // Logic to increase efficiency or cooldown based on level can be added here
    }
}