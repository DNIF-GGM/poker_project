using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private List<CardEnum> _cardSlot = new List<CardEnum>();

    private void Start(){
        CardManager.Instance.CardSlots.Add(this);    
    }

    public void SetCard(CardEnum card){
        _cardSlot.Add(card); //드래그 앤 드롭시 해당 카드의 상태를 매개변수로 등록하여 리스트 추가
    }

    public void CheckSlot(){
        if(_cardSlot.Count <= 0) return;

        _cardSlot.Sort();
        foreach(List<CardEnum> i in CardManager.Instance.HandRanking.Values){
            i.Sort();
            if(i.SequenceEqual(_cardSlot)){
                Debug.Log(CardManager.Instance.HandRanking.FirstOrDefault(x => x.Value == i).Key); //프리팹 추가 후 풀매니저 소환으로 변경
            }
            else{
                Debug.Log("어캐 돌아가지..?");
            }
            _cardSlot.Clear();
        }
    }
}
