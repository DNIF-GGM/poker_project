using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class UserSetting
{
    [JsonProperty("sfxVolume")] public float sfxVolume;
    [JsonProperty("bgmVolume")] public float bgmVolume;
}
