using UnityEngine;

public class Card : PoolableMono {
    
    [SerializeField] private CardEnum _cardEnum;
    [SerializeField] private Material _cardMat;
    public CardEnum cardEnum {get => _cardEnum;}
    public Material cardMat {get => _cardMat;}

    public CardSO cardSO {get; private set;}

    public void CardStatusSet(CardSO cardSO){
        this.cardSO = cardSO;

        _cardEnum = cardSO.cardEnum;
        _cardMat = cardSO.mat;

        GetComponent<MeshRenderer>().material = _cardMat;
    }

    public void Init(Vector3 pos, Transform parent)
    {
        transform.position = pos;
        transform.SetParent(parent);
    }

    public override void Reset()
    {
        
    }
}