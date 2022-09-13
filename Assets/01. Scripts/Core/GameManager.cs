using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private List<PoolableMono> _poolingList;

    private bool _gameOver = false;
    public bool GameOver { get => _gameOver; set => _gameOver = value; }

    private void Awake() {
        if(Instance == null) Instance = this;
        // if(Instance != null) { Debug.LogWarning("Multiple GameManager Instance is Running, Destroy This"); Destroy(gameObject); return; }
        // else { Instance = this; DontDestroyOnLoad(transform.root.gameObject); }

        PoolManager.Instance = new PoolManager(transform.GetChild(0));
        foreach(PoolableMono p in _poolingList){
            PoolManager.Instance.CreatePool(p, 10);
        }

        CoinManager.Instance = gameObject.AddComponent<CoinManager>();
        SlotManager.Instance = gameObject.AddComponent<SlotManager>();
        CardManager.Instance = gameObject.GetComponent<CardManager>();
        StageManager.Instance = gameObject.GetComponent<StageManager>();
        CoinManager.Instance = gameObject.AddComponent<CoinManager>();
    }

    public bool CardGenealogy(List<CardEnum> cards, out string name){
        bool value = false;
        string returnname = null;

        if(Royal(cards, out returnname)) value = true;
        else if(StraightFlush(cards, out returnname)) value = true;
        else if(FourCard(cards, out returnname)) value = true;
        else if(Flush(cards, out returnname)) value = true;
        else if(Straight(cards, out returnname)) value = true;
        else if(Triple(cards, out returnname)) value = true;
        else if(TwoPair(cards, out returnname)) value = true;
        else if(OnePair(cards, out returnname)) value = true;

        name = returnname;
        return value;
    }

    private bool Royal(List<CardEnum> cards, out string name){
        name = null;

        if(cards.Count != 5) return false;

        ShapeEnum standardEnum = cards[0].shapeEnum;

        NumberEnum[] answerNums = new NumberEnum[5] {NumberEnum.ten, NumberEnum.jack, NumberEnum.queen, NumberEnum.king, NumberEnum.one};
        NumberEnum[] cardNums = new NumberEnum[5];


        for(int i = 0; i < cards.Count; i++){
            if(cards[i].shapeEnum != standardEnum) return false;

            cardNums[i] = cards[i].numberEnum;
        }
        
        if(cardNums.SequenceEqual(answerNums)){
            name = "Royal";
            Debug.Log("name");
            return true;
        }

        return false;
    }

    private bool StraightFlush(List<CardEnum> cards, out string name){
        name = null;

        if (cards.Count != 5) return false;

        ShapeEnum standardEnum = cards[0].shapeEnum;

        if(cards[0].numberEnum > NumberEnum.nine) return false;

        NumberEnum[] answerNums = new NumberEnum[5] { cards[0].numberEnum, cards[0].numberEnum + 1, cards[0].numberEnum + 2, cards[0].numberEnum + 3, cards[0].numberEnum + 4 };
        NumberEnum[] cardNums = new NumberEnum[5];


        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].shapeEnum != standardEnum) return false;

            cardNums[i] = cards[i].numberEnum;
        }

        if(cardNums.SequenceEqual(answerNums)){
            name = "StraightFlush";
            return true;
        }

        return false;
    }

    private bool FourCard(List<CardEnum> cards, out string name){
        name = null;
        if(cards.Count != 4) return false;

        NumberEnum standardNum = cards[0].numberEnum;

        for(int i = 0; i < cards.Count; i++){
            if(cards[i].numberEnum != standardNum) return false;
        }

        name = "FourCard";
        return true;
    }

    private bool Flush(List<CardEnum> cards, out string name){
        name = null;
        if(cards.Count != 5) return false;

        ShapeEnum standardShape = cards[0].shapeEnum;

        for(int i = 0; i < cards.Count; i++){
            if(cards[i].shapeEnum != standardShape) return false;
        }

        name = "Flush";
        return true;
    }

    private bool Straight(List<CardEnum> cards, out string name){
        name = null;
        if (cards.Count != 5) return false;

        if(cards[0].numberEnum > NumberEnum.nine) return false;

        NumberEnum[] answerNums = new NumberEnum[5] { cards[0].numberEnum, cards[0].numberEnum + 1, cards[0].numberEnum + 2, cards[0].numberEnum + 3, cards[0].numberEnum + 4 };
        NumberEnum[] cardNums = new NumberEnum[5];


        for (int i = 0; i < cards.Count; i++)
        {
            cardNums[i] = cards[i].numberEnum;
        }

        if(cardNums.SequenceEqual(answerNums)){
            name = "Straight";
            return true;
        }

        return false;
    }

    private bool Triple(List<CardEnum> cards, out string name){
        name = null;
        if(cards.Count != 3) return false;

        NumberEnum standardNum = cards[0].numberEnum;

        for(int i = 0; i < cards.Count; i++){
            if(cards[i].numberEnum != standardNum) return false;
        }

        name = "Triple";
        return true;
    }

    private bool TwoPair(List<CardEnum> cards, out string name){
        name = null;
        if(cards.Count != 4) return false;

        NumberEnum firstStandardNum = cards[0].numberEnum;
        NumberEnum secondStandardNum = cards[3].numberEnum;

        for(int i = 0; i < cards.Count; i++){
            if(cards[i].numberEnum != (i <= 1 ? firstStandardNum : secondStandardNum)) return false;
        }

        name = "TwoPair";
        return true;
    }

    private bool OnePair(List<CardEnum> cards, out string name){
        name = null;
        if(cards.Count != 2) return false;

        NumberEnum standardNum = cards[0].numberEnum;

        for(int i = 0; i < cards.Count; i++){
            if(cards[i].numberEnum != standardNum) return false;
        }

        name = "OnePair";
        return true;
    }
}
