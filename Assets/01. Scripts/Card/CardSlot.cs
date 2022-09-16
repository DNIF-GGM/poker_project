// using System.Runtime.InteropServices;
// using System.Diagnostics.Tracing;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private List<CardEnum> _cardSlot = new List<CardEnum>();
    public List<CardEnum> _CardSlot => _cardSlot;
    [SerializeField] private Vector3 cardSpawnFactor = new Vector3();
    [SerializeField] private int maxCardCount = 5;

    private void Start(){
        CardManager.Instance.CardSlots.Add(this);    
    }

    public void SetCard(){
        EventSystem.current.SetSelectedGameObject(null);

        if(SlotManager.Instance.currentSelectedCard == null) return;

        if(transform.childCount - 1 >= maxCardCount) return;

        if(!CoinManager.Instance.TryBettingCoin(SlotManager.Instance.currentSelectedCard.cardSO.cost)) 
        {
            Debug.Log("돈이 없다 이자식아");
            return;
        }
        Card card = PoolManager.Instance.Pop("Card") as Card;
        card.GetComponent<RectTransform>().localScale = new Vector3(30, 30, 30);
        card.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180);
        
        Vector3 spawnPos = transform.position;
        spawnPos.x += (-maxCardCount / 2 + transform.childCount) * cardSpawnFactor.x;
        spawnPos.y += cardSpawnFactor.y;
        spawnPos.z += cardSpawnFactor.z;
        
        card.Init(spawnPos, this.transform);
        card.CardStatusSet(SlotManager.Instance.currentSelectedCard.cardSO);

        //카드를 슬롯에서 슬롯으로 옮길때 리스트에서 삭제되게 해야함
        _cardSlot.Add(SlotManager.Instance.currentSelectedCard.cardEnum); //드래그 앤 드롭시 해당 카드의 상태를 매개변수로 등록하여 리스트 추가

        Cursor.visible = true;
        card.isOnSlot = true;
        card.Slot = this;
        SlotManager.Instance.isDrag = false;
        SlotManager.Instance.ResetSelectedCard();
    }

    public void ClearSlot()
    {
        //원래 방식대로 하면 transform.childCount는 for문이 진행됨에 따라 바뀌는 값이기에 재대로 for문이 수행되지 않음
        //for(int i = 0; i < transform.childCount; i ++) //카드들 모두 없애기
        //{
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++){
            Card card = transform.GetChild(0).GetComponent<Card>();
            PoolManager.Instance.Push(card);
        }

        _cardSlot.Clear();
    }

    public void CheckSlot(){
        if(_cardSlot.Count <= 0) return;

        string outname = null;
        if(GameManager.Instance.CardGenealogy(_cardSlot, out outname)){
            if(outname != null)
            {
                UnitBase unit = PoolManager.Instance.Pop(outname) as UnitBase;
                unit.transform.position = transform.position;
                unit.transform.rotation = Quaternion.identity;
                StageManager.Instance.Units.Add(unit);
            }
        }

        ClearSlot();
    }
}
