using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class UserSetting
{
    [JsonProperty("sfxVolume")] public int sfxVolume;
    [JsonProperty("bgmVolume")] public int bgmVolume;
}
