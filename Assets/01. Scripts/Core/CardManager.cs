using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance = null;

    [SerializeField] private List<CardSO> _cardSo = new List<CardSO>();

    private List<CardSlot> _cardSlots = new List<CardSlot>();
    public List<CardSlot> CardSlots { get => _cardSlots; set => _cardSlots = value; }

    private Transform _cardParentTrm;

    private bool _isSet = false;
    public bool IsSet {get => _isSet;}

    private bool isReroll = false;

    public int RerollCost = 5;

    private void Awake()
    {
        _cardParentTrm = GameObject.Find("CardCanvas").transform;
    }

    private void Update() {
        //test
        if(Input.GetKeyDown(KeyCode.I)){
            CardSpawn();
        }

        if(Input.GetKeyDown(KeyCode.K)){
            SpawnUnit();
        }
    }

    public void ClearCard()
    {
        int childCount = _cardParentTrm.childCount;
        for(int i = 0; i < childCount; i++)
        {
            Card card = _cardParentTrm.GetChild(0).GetComponent<Card>();
            PoolManager.Instance.Push(card);
        }
    }

    public void RerollCard()
    {
        if(isReroll) return;
        if(CoinManager.Instance.CurrentCoin - RerollCost < 0)
        {
            Debug.Log("돈이 없다 이자식다");
            return;
        }

        CoinManager.Instance.PayCoin(RerollCost);
        CardSpawn();
    }

    public void CardSpawn(){
        if(isReroll) return;

        ClearCard();

        foreach(CardSlot cs in _cardSlots)
            cs.ClearSlot();

        StartCoroutine(CardSpawnCoroutine());
    }

    IEnumerator CardSpawnCoroutine(){
        isReroll = true;
        for(int i = 0; i < 13; i++){
            RectTransform rect;

            Card card = PoolManager.Instance.Pop("Card") as Card;
            rect = card.GetComponent<RectTransform>();
            rect.SetParent(_cardParentTrm);
            rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, rect.anchoredPosition.y, 0);
            rect.localRotation = Quaternion.Euler(0, 0, 180);
            rect.localScale = new Vector3(100, 100, 100);
            
            card.CardStatusSet(_cardSo[Random.Range(0, 52)]);

            yield return new WaitForSecondsRealtime(0.02f);
        }
        isReroll = false;
    }

    public void SpawnUnit()
    {
        foreach (CardSlot c in _cardSlots)
        {
            c.CheckSlot();
        }
        _isSet = true;
    }
}
