using UnityEngine;

public class ActiveSkill : MonoBehaviour, ISkill
{
    private int skillId;
    private int level;

    [SerializeField]
    protected float cooldown = 1f;
    [SerializeField]
    protected int sound_ID_1;   // 메인 사운드 ID
    [SerializeField]
    protected int sound_ID_2;   // 예비 사운드 ID

    // 반복 사운드용
    protected bool PlayingSound = false;

    public int SkillId => skillId;
    public float Cooldown => cooldown;
    public int Level => level;
    public SkillType SkillType => SkillType.Active;

    public virtual void Init(
        int skillId,
        int level,
        float cooldown)
    {
        this.skillId = skillId;
        this.level = level;
        this.cooldown = cooldown;
    }

    public virtual void Upgrade()
    {
    }

    public virtual void StartLifeCycle()
    {
        Debug.Log($"Starting lifecycle of {GetType()}");
    }

    protected virtual void PlaySound1(float volume = 1.0f, float pitch = 1.0f)
    {
        SoundManager.Instance.PlaySFX(sound_ID_1, volume, pitch);
        PlayingSound = true;
    }

    protected virtual void PlaySound2(float volume = 1.0f, float pitch = 1.0f)
    {
        SoundManager.Instance.PlaySFX(sound_ID_1, volume, pitch);
        SoundManager.Instance.PlaySFX(sound_ID_2, volume, pitch);
        PlayingSound = true;
    }

    protected virtual void PlayAllSound(float volume = 1.0f, float pitch = 1.0f)
    {
        SoundManager.Instance.PlaySFX(sound_ID_1, volume, pitch);
        SoundManager.Instance.PlaySFX(sound_ID_2, volume, pitch);
        PlayingSound = true;
    }

    protected virtual void StopSound(int id)
    {
        SoundManager.Instance.StopSFX(id);
        PlayingSound = false;
    }

    protected virtual void StopSound(int id1, int id2)
    {
        SoundManager.Instance.StopSFX(id1);
        SoundManager.Instance.StopSFX(id2);
        PlayingSound = false;
    }
}