using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class UserSetting
{
    [JsonProperty("effectVolume")] public int effectVolume;
    [JsonProperty("systemVolume")] public int systemVolume;
    [JsonProperty("bgmVolume")] public int bgmVolume;
    [JsonProperty("masterVolume")] public int masterVolume;
}
