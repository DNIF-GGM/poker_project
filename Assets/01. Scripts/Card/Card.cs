using UnityEngine;
using DG.Tweening;

public class Card : PoolableMono {
    
    [SerializeField] private CardEnum _cardEnum;
    [SerializeField] private NumberEnum _numberEnum;
    [SerializeField] private Material _cardMat;

    public CardEnum cardEnum {get => _cardEnum;}
    public Material cardMat {get => _cardMat;}
    public CardSlot Slot { get; set; } = null;

    public Vector3 streamVector;

    public CardSO cardSO {get; private set;}

    public bool isOnSlot {get; set;} = false;

    private void OnEnable() {
        isOnSlot = false;
    }

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

    private void OnMouseEnter() {
        Vector3 scale = GetComponent<RectTransform>().localScale;

        if(!SlotManager.Instance.isDrag) GetComponent<RectTransform>().DOScale(isOnSlot ? 55 : 120, 0.5f);
    }
    private void OnMouseExit() {
        Vector3 scale = GetComponent<RectTransform>().localScale;

        if(!SlotManager.Instance.isDrag) GetComponent<RectTransform>().DOScale(isOnSlot ? 30 : 100, 0.5f);
    }

    public void ResetPos()
    {
        
    }

    public override void Reset()
    {
    }
}