using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _escPanel;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private GameObject _onMuteImg;

    private bool _isPause = false;
    private bool _onSetting = false;

    private void Start()
    {
        _bgmSlider.value = DataManager.Instance.userSetting.bgmVolume;
        _sfxSlider.value = DataManager.Instance.userSetting.sfxVolume;
    }

    private void Update() {
        Time.timeScale = (_isPause) ? 0f : 1f;

        if(Input.GetKeyDown(KeyCode.Escape)){
            _isPause = !_isPause;

            if(_isPause){
                AudioManager.Instance.PauseBGM(true);
                _escPanel.SetActive(true);
            }
            else{
                if(_onSetting){
                    _settingPanel.SetActive(false);
                    _isPause = true;
                    _onSetting = false;
                }
                else{
                    AudioManager.Instance.PauseBGM(false);
                    _escPanel.SetActive(false);
                }
            }
        }

        AudioSetting();
    }

    private void AudioSetting()
    {
        if (_bgmSlider.value == _bgmSlider.minValue)
        {
            AudioManager.Instance.masterMixer.SetFloat("BGM", -80);
            DataManager.Instance.userSetting.bgmVolume = -80;
        } 
        else
        {
            AudioManager.Instance.masterMixer.SetFloat("BGM", _bgmSlider.value);
            DataManager.Instance.userSetting.bgmVolume = _bgmSlider.value;
        }
        if (_sfxSlider.value == _sfxSlider.minValue)
        {
            AudioManager.Instance.masterMixer.SetFloat("SFX", -80);
            DataManager.Instance.userSetting.sfxVolume = -80;
        }
        else 
        {
            AudioManager.Instance.masterMixer.SetFloat("SFX", _sfxSlider.value);
            DataManager.Instance.userSetting.sfxVolume = _sfxSlider.value;
        }
    }

    public void Continue(){
        _escPanel.SetActive(false);
        _isPause = false;
    }

    public void SettingPanel(){
        _settingPanel.SetActive(true);
        _onSetting = true;
    }

    public void Exit(){
        if(_onSetting) _settingPanel.SetActive(false);
        else{
            SceneLoader.Instance.LoadScene("Intro");
        }
    }

    public void Mute(){
        AudioManager.Instance.onMute = !AudioManager.Instance.onMute;

        _onMuteImg.SetActive(AudioManager.Instance.onMute);
    }
}
