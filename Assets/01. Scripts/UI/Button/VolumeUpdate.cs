using System.Diagnostics.Contracts;
using System.Reflection;
using TMPro;
using UnityEngine;

public class VolumeUpdate : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI effectValueTxt, systemValueTxt, bgmValueTxt, masterValueTxt;
    // private UserSetting userSetting = null;

    // private void Start()
    // {
    //     userSetting = DataManager.Instance.userSetting;
    //     effectValueTxt.text = $"{userSetting.effectVolume}";
    //     systemValueTxt.text = $"{userSetting.systemVolume}";
    //     bgmValueTxt.text = $"{userSetting.bgmVolume}";
    //     masterValueTxt.text = $"{userSetting.masterVolume}";
    // }

    // public void EffectVolumeUpdate(int amount)
    // {
    //     if (userSetting.effectVolume + amount > 10 || userSetting.effectVolume + amount < 0) return;

    //     userSetting.effectVolume += amount;
    //     effectValueTxt.text = $"{(userSetting.effectVolume)}";
    // }

    // public void SystemVolumeUpadte(int amount)
    // {
    //     if (userSetting.systemVolume + amount > 10 || userSetting.systemVolume + amount < 0) return;

    //     userSetting.systemVolume += amount;
    //     systemValueTxt.text = $"{(userSetting.systemVolume)}";
    // }

    // public void BGMVolumeUpdate(int amount)
    // {
    //     if (userSetting.bgmVolume + amount > 10 || userSetting.bgmVolume + amount < 0) return;

    //     userSetting.bgmVolume += amount;
    //     bgmValueTxt.text = $"{(userSetting.bgmVolume)}";
    // }

    // public void MasterVolumeUpdate(int amount)
    // {
    //     if (userSetting.masterVolume + amount > 10 || userSetting.masterVolume + amount < 0) return;

    //     userSetting.masterVolume += amount;
    //     masterValueTxt.text = $"{(userSetting.masterVolume)}";
    // }
}
