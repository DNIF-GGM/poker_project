using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] List<AudioClip> clipList = new List<AudioClip>();
    [field: SerializeField] public AudioMixer masterMixer {get; private set;}
    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    private AudioSource systemAudioPlayer = null;
    private AudioSource bgmAudioPlayer = null;

    public bool onMute {get; set;} = false;

    private void Awake()
    {
        if(Instance != null) { Debug.LogWarning("Multiple AudioManager Instance is Running, Destroy This"); Destroy(gameObject); return; }
        else { Instance = this; DontDestroyOnLoad(transform.root.gameObject); }

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

    private void Start()
    {
        masterMixer.SetFloat("BGM", DataManager.Instance.userSetting.bgmVolume);
        masterMixer.SetFloat("SFX", DataManager.Instance.userSetting.sfxVolume);
    }

    private void Update() {
        masterMixer.SetFloat("MasterVolume", (onMute ? -80f : 0));
    }

    public void PlayEffectAudio(string clipName, AudioSource player)
    {
        if(clipName.Length <= 0)
        {
            player.Pause();
            return;
        } 

        player.clip = null;
        player.clip = clips[clipName];
        player.Play();
    }

    public void PlaySystemAudio(string clipName)
    {
        if(clipName.Length <= 0)
        {
            systemAudioPlayer.Pause();
            return;
        } 

        systemAudioPlayer.clip = null;
        systemAudioPlayer.clip = clips[clipName];
        systemAudioPlayer.Play();
    }

    public void PlayBGMAudio(string clipName)
    {
        bgmAudioPlayer.clip = null;
        bgmAudioPlayer.clip = clips[clipName];
        bgmAudioPlayer.Play();
    }

    public void PauseBGM(bool isPause)
    {
        if(isPause)
            bgmAudioPlayer.Pause();
        else
            bgmAudioPlayer.Play();
    }
}
