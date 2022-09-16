using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _escPanel;
    [SerializeField] private GameObject _settingPanel;

    [SerializeField] private GameObject _onMuteImg;

    private bool _isPause = false;
    private bool _onSetting = false;

    private void Update() {
        Time.timeScale = (_isPause) ? 0f : 1f;

        if(Input.GetKeyDown(KeyCode.Escape)){
            _isPause = !_isPause;

            if(_isPause){
                _escPanel.SetActive(true);
            }
            else{
                if(_onSetting){
                    _settingPanel.SetActive(false);
                    _isPause = true;
                    _onSetting = false;
                }
                else{
                    _escPanel.SetActive(false);
                }
            }
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
        (_onSetting ? _settingPanel : _escPanel).gameObject.SetActive(false);
    }

    public void Mute(){
        AudioManager.Instance.onMute = !AudioManager.Instance.onMute;

        _onMuteImg.SetActive(AudioManager.Instance.onMute);
    }
}
