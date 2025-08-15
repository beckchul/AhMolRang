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

            var activeSkillData = SkillManager.Instance.GetActiveSkillData(skillStatus.SkillId);

            if (activeSkillData != null)
            {
                activeSkillIcons[activeSkillIndex].gameObject.SetActive(true);
                activeSkillIcons[activeSkillIndex].UpdateIcon(activeSkillData.skillIcon, activeSkillData.skillLevel);

                activeSkillIndex++;
            }

            var pasiveSkillData = SkillManager.Instance.GetPasiveSkillData(skillStatus.SkillId);

            if (pasiveSkillData != null)
            {
                pasiveSkillIcons[pasiveSkillIndex].gameObject.SetActive(true);
                pasiveSkillIcons[pasiveSkillIndex].UpdateIcon(pasiveSkillData.skillIcon, pasiveSkillData.skillLevel);

                pasiveSkillIndex++;
            }
        }
    }
}
