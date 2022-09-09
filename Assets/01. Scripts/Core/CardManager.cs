using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance = null;

    [SerializeField] private List<CardSO> _cardSo = new List<CardSO>();

    [field: SerializeField] //수정 후
    public List<CardGenealogy> Genealogies { get; private set; } = new List<CardGenealogy>();

    #region 수정 전
    // private Dictionary<string, List<CardEnum>> _handRankings = new Dictionary<string, List<CardEnum>>(){
    //     {"족보이름", new List<CardEnum>() {CardEnum.Spade_One, CardEnum.Spade_Two, CardEnum.Spade_Three}} //족보 생기면 추가 예정
    // };
    // public Dictionary<string, List<CardEnum>> HandRanking { get => _handRankings; }
    #endregion

    private List<CardSlot> _cardSlots = new List<CardSlot>();
    public List<CardSlot> CardSlots { get => _cardSlots; set => _cardSlots = value; }

    private Transform _cardParentTrm;

    private bool _isSet = false;
    public bool IsSet {get => _isSet;}

    private void Awake()
    {
        _cardParentTrm = GameObject.Find("CardCanvas/Cards").transform;

        foreach (CardGenealogy g in Genealogies)
            g.combi.Sort();
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
        for(int i = 0; i < _cardParentTrm.childCount; i++)
        {
            CardUI cardUi = _cardParentTrm.GetChild(i).GetComponent<CardUI>();
            PoolManager.Instance.Push(cardUi);
        }
    }

    public void CardSpawn(){
        ClearCard();

        foreach(CardSlot cs in _cardSlots)
            cs.ClearSlot();

        for(int i = 0; i < 13; i++){
            CardUI cardUi = PoolManager.Instance.Pop("Card(UI)") as CardUI;
            cardUi.transform.SetParent(_cardParentTrm);
            cardUi.transform.localScale = new Vector3(0.7f, 1, 1);
            cardUi.InitSetting(_cardSo[Random.Range(0, 53)]);
        }
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
