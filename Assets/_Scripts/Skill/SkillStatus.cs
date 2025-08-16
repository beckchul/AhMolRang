public class SkillStatus
{
    public int SkillId { get; }
    public int Level { get; set; }

    public SkillStatus(int skillId)
    {
        SkillId = skillId;
        Level = 0;
    }

    public void LevelUp()
    {
        Level++;
    }
}