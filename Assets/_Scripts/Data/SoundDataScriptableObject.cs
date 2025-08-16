using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData")]
public class SoundDataScriptableObject : ScriptableObject
{
    [Header("Sound Info")]
    public GameType GameType;
    public SFXType SFXType;
    public List<AudioClip> AudioClipList;
    [Range(0.0001f, 1.0f)]
    public float masterVolume;
}
