// using System.Runtime.InteropServices;
// using System.Diagnostics.Tracing;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private List<CardEnum> _cardSlot = new List<CardEnum>();
    [SerializeField] private Vector3 cardSpawnFactor = new Vector3();
    [SerializeField] private Vector3 unitSpawnPos = new Vector3();
    [SerializeField] private int maxCardCount = 5;

    private void Start(){
        CardManager.Instance.CardSlots.Add(this);    
    }

    public void SetCard(){
        EventSystem.current.SetSelectedGameObject(null);

        if(SlotManager.Instance.currentSelectedCard == null) return;

        if(transform.childCount - 1 >= maxCardCount) return;

        _cardSlot.Add(SlotManager.Instance.currentSelectedCard.CardEnum); //드래그 앤 드롭시 해당 카드의 상태를 매개변수로 등록하여 리스트 추가
        Card card = PoolManager.Instance.Pop("Card") as Card;
        
        Vector3 spawnPos = transform.position;
        spawnPos.x += (-maxCardCount / 2 + transform.childCount) * cardSpawnFactor.x;
        spawnPos.y += cardSpawnFactor.y;
        spawnPos.z += cardSpawnFactor.z;
        card.Init(spawnPos, this.transform);

        SlotManager.Instance.ResetSelectedCard();
    }

    public void ClearSlot()
    {
        for(int i = 0; i < transform.childCount; i ++) //카드들 모두 없애기
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            PoolManager.Instance.Push(card);
        }

        _cardSlot.Clear();
    }

    public void CheckSlot(){
        if(_cardSlot.Count <= 0) return;

        _cardSlot.Sort();

        #region 수정 후 로직
        foreach(CardGenealogy g in CardManager.Instance.Genealogies)
        {
            if(g.combi.SequenceEqual(_cardSlot)) {
                string genealogy = g.genealogyName;
                Debug.Log(genealogy);
                //Unit unit = PoolManager.Instance.Pop(genealogy) as Unit;
                //unit.transform.position = unitSpawnPos;
            }
        }

        ClearSlot();
        #endregion

        #region 수정 전 로직 (이렇게 하면 메모리 아야할 거 같아서 위 로직으로 바꿈 확인 부탁)
        // foreach(List<CardEnum> i in CardManager.Instance.HandRanking.Values){
        //     string genealogy;
        //     i.Sort();
        //     if(i.SequenceEqual(_cardSlot)){
        //         genealogy = CardManager.Instance.HandRanking.FirstOrDefault(x => x.Value == i).Key;
        //         //PoolManager.Instance.Pop(genealogy);
        //         Debug.Log(genealogy);
        //     }
        //     // else{
        //     //     Debug.Log("어캐 돌아가지..?");    <- 없어도 될 듯 그냥 위에서 모든 카드들 다 날려버림
        //     // }
        //     _cardSlot.Clear();
        // }
        #endregion
    }
}
