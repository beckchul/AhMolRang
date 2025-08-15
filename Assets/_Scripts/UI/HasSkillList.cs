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
        int ActiveSkillCount = 3;

        foreach(var (activeSkillIcon, index) in activeSkillIcons.Select((value, idx) => (value, idx)))
        {
            if (index < ActiveSkillCount)
            {
                activeSkillIcon.gameObject.SetActive(true);
            }
            else
            {
                activeSkillIcon.gameObject.SetActive(false);
            }
        }

        int PasiveSkillCount = 2;

        foreach (var (pasiveSkillIcon, index) in pasiveSkillIcons.Select((value, idx) => (value, idx)))
        {
            if (index < PasiveSkillCount)
            {
                pasiveSkillIcon.gameObject.SetActive(true);
            }
            else
            {
                pasiveSkillIcon.gameObject.SetActive(false);
            }
        }
    }
}
