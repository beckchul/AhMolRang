using UnityEngine;
using System.Collections.Generic;

public class MonsterDeadSound : MonoBehaviour
{
    [SerializeField]
    private List<int> soundIDs;
    [SerializeField]
    private MonsterBase monster;

    private int RandomSoundID()
    {
        return Random.Range(0, soundIDs.Count);
    }

    public void DeadSound()
    {
        SoundManager.Instance.PlaySFX(soundIDs[RandomSoundID()]);
    }

}
