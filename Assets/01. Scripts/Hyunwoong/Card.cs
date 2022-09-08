using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public abstract class Card : MonoBehaviour {
    public CardEnum _cardEnum;
    public float _cardCost;
    public Sprite _cardSprite;

    public CardEnum SetCardEnum(int cardNum){
        if(cardNum == 52) _cardEnum = CardEnum.Joker;
        else{
            _cardEnum = (CardEnum)cardNum;
        }

        return _cardEnum;
    }

    public void InitSettion(){
        
    }
}