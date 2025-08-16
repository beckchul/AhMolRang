using System.Linq;
using UnityEngine;

public class HasSkillList : MonoBehaviour
{
    [SerializeField]
    private SkillIcon[] activeSkillIcons;

    [SerializeField]
    private SkillIcon[] pasiveSkillIcons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHasSkillList()
    {
        // 전체 아이콘 비활성화
        foreach (SkillIcon skillIcon in activeSkillIcons)
        {
            skillIcon.gameObject.SetActive(false);
        }

        foreach (SkillIcon skillIcon in pasiveSkillIcons)
        {
            skillIcon.gameObject.SetActive(false);
        }

        // 들고있는 스킬들 아이콘 활성화
        int activeSkillIndex = 0;
        int pasiveSkillIndex = 0;
        var playerSkills = SkillManager.Instance.Skills;

        foreach (var skill in playerSkills)
        {
            var skillStatus = skill.Value;

            if (skillStatus.Level == 0)
            {
                continue;
            }

            var activeSkillStatus = SkillManager.Instance.GetActiveSkillStatus(skillStatus.SkillId);
            var icon = SkillManager.Instance.GetSkillIcon(skillStatus.SkillId);

            if (activeSkillStatus != null)
            {
                activeSkillIcons[activeSkillIndex].gameObject.SetActive(true);
                activeSkillIcons[activeSkillIndex].UpdateIcon(icon, activeSkillStatus.Level);

                activeSkillIndex++;
            }

            var pasiveSkillStatus = SkillManager.Instance.GetPasiveSkillStatus(skillStatus.SkillId);

            if (pasiveSkillStatus != null)
            {
                pasiveSkillIcons[pasiveSkillIndex].gameObject.SetActive(true);
                pasiveSkillIcons[pasiveSkillIndex].UpdateIcon(icon, pasiveSkillStatus.Level);

                pasiveSkillIndex++;
            }
        }
    }
}
