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

    protected const int MonsterLayerMask = 1 << 8;
    public SkillType SkillType => SkillType.Active;

    public virtual void Init(
        int skillId,
        int level,
        float efficiency,
        float cooldown)
    {
        this.skillId = skillId;
        this.level = level;
        this.efficiency = efficiency;
        this.cooldown = cooldown;
    }

    public virtual void Upgrade()
    {
    }

    public virtual void Use()
    {
    }
}