using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SkillManager>
{
    public enum BGMType
    {
        Stage = 0
    }

    public enum SFXType
    {

    }

    //audio clip 담을 수 있는 배열
    [SerializeField] private AudioClip[] bgms;
    [SerializeField] private AudioClip[] sfxs;

    //플레이하는 AudioSource
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioBgm;
    [SerializeField] private AudioSource audioSfx;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;


    public void PlayBGM(BGMType bgmType)
    {
        audioBgm.clip = bgms[(int)bgmType];
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    public void PlaySFX(SFXType sfxType, float volume = 1.0f, float pitch = 1.0f)
    {
        audioSfx.pitch = pitch;
        audioSfx.PlayOneShot(sfxs[(int)sfxType], volume);
    }

    public void StopSFX()
    {
        audioSfx.Stop();
    }

    // 볼륨 조정
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    // 음소거 예시
    public void MuteAll(bool isMuted)
    {
        audioMixer.SetFloat("MasterVolume", isMuted ? -80f : 0f); // -80dB는 거의 무음
    }
}
