using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData")]
public class SoundDataScriptableObject : ScriptableObject
{
    [Header("Sound Info")]
    public int ID;
    public AudioClip AudioClip;
    [Range(0.0001f, 1.0f)]
    public float masterVolume = 1.0f;
    public bool Loop = false;
}
