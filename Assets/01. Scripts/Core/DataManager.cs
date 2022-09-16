using System.Linq;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    public UserData userData = null;
    public UserSetting userSetting = null;

    private void Awake()
    {
        if(Instance != null) { Debug.LogWarning("Multiple DataManager Instance is Running, Destroy This"); Destroy(gameObject); return; }
        else { Instance = this; DontDestroyOnLoad(transform.root.gameObject); }
        
        if(!TryReadJson<UserData>(out userData))
            userData = new UserData() {};
        if(!TryReadJson<UserSetting>(out userSetting))
            userSetting = new UserSetting() { sfxVolume = 5, masterVolume = 5, bgmVolume = 5 };
    }

    private bool TryReadJson<T>(out T data)
    {
        string json = File.ReadAllText(GetPath<T>()); 

        if (json.Length > 0)
        {
            data = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
        else
        {
            data = default(T);
            return false;
        }
    }

    private void SaveData<T>(T data)
    {
        string json = JsonConvert.SerializeObject(data);

        File.WriteAllText(GetPath<T>(), json);
    }

    private string GetPath<T>()
    {
        //빌드할 때 경로 Application.persistentDataPath + "/Asset" 으로 변경해야댐
        return "./Assets/04. Json/" + typeof(T) + ".json";
    }

    private void OnDisable()
    {
        SaveData<UserData>(userData);
        SaveData<UserSetting>(userSetting);
    }
}
