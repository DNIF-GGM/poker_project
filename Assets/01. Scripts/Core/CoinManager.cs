using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance = null;

    private int _currentCoin;
    public int BettedCoin { get; private set; } = 0;
    public int CurrentCoin {get => _currentCoin; set => _currentCoin = value;}

    public void PayCoin(int reduceCoin){
        _currentCoin -= reduceCoin;
        BettedCoin += reduceCoin;
        if(_currentCoin <= 0) _currentCoin = 0;
    }

    public void UpdateCoin(float multipleNum){
        _currentCoin += Mathf.RoundToInt(BettedCoin * multipleNum);
    }
}
