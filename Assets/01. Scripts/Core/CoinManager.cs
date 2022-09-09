using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance = null;

    private float _currentCoin;
    public float CurrentCoin {get => _currentCoin; set => _currentCoin = value;}

    public void PayCoin(int reduceCoin){
        _currentCoin -= reduceCoin;
        if(_currentCoin <= 0) _currentCoin = 0;
    }

    public void UpdateCoin(float multipleNum){
        _currentCoin = Mathf.RoundToInt(_currentCoin *= multipleNum);
    }
}
