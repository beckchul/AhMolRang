using UnityEngine;

public class ActiveSkill : MonoBehaviour, ISkill
{
    private int skillId;
    private int level;
    private float efficiency;
    private float cooldown;

    public int SkillId => skillId;
    public float Efficiency => efficiency;
    public float Cooldown => cooldown;
    public int Level => level;

    public SkillType SkillType => SkillType.Active;

    public void Init(
        int skillId,
        float efficiency,
        float cooldown)
    {
        this.skillId = skillId;
        this.efficiency = efficiency;
        this.cooldown = cooldown;
        level = 0;
    }

    public void Upgrade()
    {
    }

    public void Use()
    {
    }
}