using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance = null;

    private List<CardSlot> _cardSlots = new List<CardSlot>();
    public List<CardSlot> CardSlots { get => _cardSlots; set => _cardSlots = value; }

    private Dictionary<string, List<CardEnum>> _handRankings = new Dictionary<string, List<CardEnum>>(){
        {"족보이름", new List<CardEnum>() {CardEnum.Spade_One, CardEnum.Spade_Two, CardEnum.Spade_Three}} //족보 생기면 추가 예정
    };
    public Dictionary<string, List<CardEnum>> HandRanking { get => _handRankings; }

    private void Awake() {
        if(Instance == null) Instance = this; //나중에 게임매니저로 옮겨야댐
    }

    private void Update() {
        //Test
        if(Input.GetKeyDown(KeyCode.K)){
            SpawnUnit();
        }
    }

    public void SpawnUnit(){
        foreach(CardSlot c in _cardSlots){
            c.CheckSlot();
        }
    }
}
