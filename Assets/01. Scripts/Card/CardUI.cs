using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : PoolableMono
{
    [SerializeField] private CardEnum _cardEnum;
    public CardEnum CardEnum { get => _cardEnum;}
    public Material CardMat { get; private set; }

    public void InitSetting(CardSO cardSO){
        _cardEnum = cardSO.cardEnum;
        CardMat = cardSO.mat;
        GetComponent<MeshRenderer>().materials[0] = CardMat;
    }

    public override void Reset()
    {
        
    }
}
