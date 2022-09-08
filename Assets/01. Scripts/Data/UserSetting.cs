using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class UserSetting
{
    [JsonProperty("effectVolume")] public float effectVolume;
    [JsonProperty("systemVolume")] public float systemVolume;
    [JsonProperty("bgmVolume")] public float bgmVolume;
    [JsonProperty("masterVolume")] public float masterVolume;
}
