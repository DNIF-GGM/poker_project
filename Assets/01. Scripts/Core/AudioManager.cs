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

        // systemAudioPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        // bgmAudioPlayer = transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void Update() {
        masterMixer.SetFloat("MasterVolume", (onMute ? -80f : 0));
    }

    public void PlayEffectAudio(string clipName, AudioSource player)
    {
        player.clip = null;
        player.clip = clips[clipName];

        player.volume = (float)(DataManager.Instance.userSetting.effectVolume * DataManager.Instance.userSetting.masterVolume) / 100f;
    }

    public void PlaySystemAudio(string clipName)
    {
        systemAudioPlayer.clip = null;
        systemAudioPlayer.clip = clips[clipName];

        systemAudioPlayer.volume = (float)(DataManager.Instance.userSetting.systemVolume * DataManager.Instance.userSetting.masterVolume) / 100f;
    }

    public void PlayBGMAudio(string clipName)
    {
        bgmAudioPlayer.clip = null;
        bgmAudioPlayer.clip = clips[clipName];

        bgmAudioPlayer.volume = (float)(DataManager.Instance.userSetting.bgmVolume * DataManager.Instance.userSetting.masterVolume) / 100f;
    }
}
