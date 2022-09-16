using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance = null;

    private int _currentCoin = 10;
    private TextMeshProUGUI bettedCoinTMP = null;
    private TextMeshProUGUI coinInfoTMP = null;
    public int BettedCoin { get; private set; } = 0;
    public int CurrentCoin {get => _currentCoin; set => _currentCoin = value;}

    private void Awake()
    {
        bettedCoinTMP = GameObject.Find("UICanvas/BettedCoinTMP").GetComponent<TextMeshProUGUI>();
        coinInfoTMP = GameObject.Find("UICanvas/CoinTMP").GetComponent<TextMeshProUGUI>();

        coinInfoTMP.text = "Coin : " + _currentCoin;
        bettedCoinTMP.text = "Betted Coin : " + BettedCoin;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CurrentCoin += 10;
            coinInfoTMP.text = "Coin : " + _currentCoin;
        }
    }

    public bool TryBettingCoin(int value)
    {
        bool returnValue = _currentCoin - value > 0;

        if(returnValue) 
        {
            _currentCoin -= value;
            coinInfoTMP.text = "Coin : " + _currentCoin;
            CoinBetting(value);
        }

        return returnValue;
    }

    public void PayCoin(int reduceCoin){
        if(_currentCoin - reduceCoin < 0) return;

        _currentCoin -= reduceCoin;
        coinInfoTMP.text = "Coin : " + _currentCoin;
    }

    public void UpdateCoin(float multipleNum){
        _currentCoin += Mathf.RoundToInt(BettedCoin * multipleNum);
    }

    public void CoinBetting(int value)
    {
        BettedCoin += value;
        bettedCoinTMP.text = "Betted Coin : " + BettedCoin;
    }
}
