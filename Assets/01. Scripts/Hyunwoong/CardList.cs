using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    [SerializeField] private List<CardMovement> cards = new List<CardMovement>();
    [SerializeField] private Transform _handTrn = null;
    [SerializeField] private Transform _deckTrn = null;

    int _index = 0;
    int cardIndex = 0;

    private void GetCard(int index){
        index = UnityEngine.Random.Range(0, 52);

        cards[_index]._isMove = true;
        cards[_index].CardMove(_deckTrn, _handTrn);
    }

    private void ListIn(Transform card){
        cardIndex++;

        float angle = 180 * 13 / cardIndex;
    }
}
