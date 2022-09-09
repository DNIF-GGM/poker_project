using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlace : PoolableMono
{
    [SerializeField] CardPlace card;

    [SerializeField] private CardEnum _cardEnum;
    public CardEnum CardEnum { get => _cardEnum;}

    public void InitSetting(CardSO cardSO){
        _cardEnum = cardSO.cardEnum;
        //GetComponent<Image>().sprite = cardSO.sprite;
    }

    public void SelectCard()
    {
        SlotManager.Instance.currentSelectedCard = this;
    }

    public override void Reset()
    {
        
    }
}
