using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoSingleton<SoundManager>
{
    private const string sfxName = "SFX";
    private const string bgmName = "BGM";
    private const string masterName = "Master";

    //audio clip 담을 수 있는 배열
    [SerializeField] private SoundDataScriptableObject[] bgms;
    [SerializeField] private SoundDataScriptableObject[] sfxs;
    private Dictionary<int, SoundDataScriptableObject> bgmsDict = new();
    private Dictionary<int, SoundDataScriptableObject> sfxsDict = new();

    //플레이하는 AudioSource
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioBgm;
    [SerializeField] private AudioSource audioSfx;
    private Dictionary<int, AudioSource> audioSfxDict = new();
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    public override void Init()
    {
        base.Init();

        sfxs = Resources.LoadAll<SoundDataScriptableObject>("ResourcesData/Sounds");
        foreach (var sfx in sfxs)
        {
            if(!sfxsDict.ContainsKey(sfx.ID)) sfxsDict.Add(sfx.ID, sfx);
        }
    }

    public void PlayBGM(int id)
    {
        audioBgm.clip = bgmsDict[id].AudioClip;
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    public void PlaySFX(int id, float volume = 1.0f, float pitch = 1.0f)
    {
        if (!sfxsDict.ContainsKey(id)) return;

        if (sfxsDict[id].Loop)
        {
            if(volume == 1.0f) AddAndPlayAudioSourceDictionary(id, sfxsDict[id].masterVolume, pitch);
            else AddAndPlayAudioSourceDictionary(id, volume, pitch);
        }
        else
        {
            audioSfx.pitch = pitch;
            audioSfx.PlayOneShot(sfxsDict[id].AudioClip, volume);
        }
    }

    public void StopSFX(int id)
    {
        if (sfxsDict[id].Loop)
        {
            StopAudioSourceDictionary(id);
        }
        else
        {
            audioSfx.Stop();
        }
    }

    // 볼륨 조정
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(masterName, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat(bgmName, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(sfxName, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void MuteAll(bool isMuted)
    {
        audioMixer.SetFloat(masterName, isMuted ? -80f : 0f);
    }

    // About AudioSource Dictionary
    private void AddAndPlayAudioSourceDictionary(int id, float volume, float pitch)
    {
        if (!audioSfxDict.ContainsKey(id))
        {
            audioSfxDict.Add(id, Instantiate(audioSfx));
            audioSfxDict[id].clip = sfxsDict[id].AudioClip;
            audioSfxDict[id].loop = true;
            audioSfxDict[id].pitch = pitch;
            audioSfxDict[id].volume = volume;
        }

        audioSfxDict[id].Play();
    }

    private void StopAudioSourceDictionary(int id)
    {
        if (audioSfxDict.ContainsKey(id)) audioSfxDict[id].Stop();
    }
}
