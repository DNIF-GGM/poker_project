using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnManager : MonoBehaviour
{
    public static CardSpawnManager Instance = null;

    [SerializeField] private List<CardSO> _cardSOs = new List<CardSO>();
    private Transform _cardParentTrm;

    private void Start() {
        _cardParentTrm = GameObject.Find("Canvas/Cards").transform;
    }

    private void Update() {
        //test
        if(Input.GetKeyDown(KeyCode.I)){
            CardSpawn();
        }
    }

    public void CardSpawn(){
        for(int i = 0; i < 13; i++){
            CardPlace cardUi = PoolManager.Instance.Pop("Card(UI)") as CardPlace;
            cardUi.transform.SetParent(_cardParentTrm);
            cardUi.transform.localScale = new Vector3(0.7f, 1, 1);
            cardUi.InitSetting(_cardSOs[Random.Range(0, 53)]);
        }
    }
}
