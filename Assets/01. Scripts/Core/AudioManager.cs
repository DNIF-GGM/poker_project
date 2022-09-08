using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] List<AudioClip> clipList = new List<AudioClip>();
    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    private AudioSource systemAudioPlayer = null;
    private AudioSource bgmAudioPlayer = null;

    private void Awake()
    {
        if(Instance != null) { Debug.LogWarning("Multple " + this.GetType() + " Instance is Running, Destroy This"); Destroy(gameObject); }
        else { Instance = this; }

        foreach(AudioClip ac in clipList)
        {
            if(clips.ContainsKey(ac.name))
            {
                Debug.LogWarning("Current Name of AudioClip is Already Exist on Clips, Return");
                continue;
            }

            clips.Add(ac.name, ac);
        }

        systemAudioPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        bgmAudioPlayer = transform.GetChild(1).GetComponent<AudioSource>();
    }

    public void PlayEffectAudio(string clipName, AudioSource player)
    {
        player.clip = null;
        player.clip = clips[clipName];

        player.volume = DataManager.Instance.userSetting.effectVolume * DataManager.Instance.userSetting.masterVolume;
    }

    public void PlaySystemAudio(string clipName)
    {
        systemAudioPlayer.clip = null;
        systemAudioPlayer.clip = clips[clipName];

        systemAudioPlayer.volume = DataManager.Instance.userSetting.systemVolume * DataManager.Instance.userSetting.masterVolume;
    }

    public void PlayBGMAudio(string clipName)
    {
        bgmAudioPlayer.clip = null;
        bgmAudioPlayer.clip = clips[clipName];

        bgmAudioPlayer.volume = DataManager.Instance.userSetting.bgmVolume * DataManager.Instance.userSetting.masterVolume;
    }
}
