using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instace = null;

    [SerializeField] private GameObject _victory;
    [SerializeField] private GameObject _defeat;

    public void GameResult(bool isVictory){
        if(isVictory){
            _victory.SetActive(true);
            Time.timeScale = 1f;
        }
        else{
            _defeat.SetActive(true);
            Time.timeScale = 1f;
            GameManager.Instance.GameOver = true;
        }
    }

    public void NextStage(){
        _victory.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ExitToMain(){
        SceneLoader.Instance.LoadScene("Intro");
    }
}
