using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : PoolableMono
{
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
