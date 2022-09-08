using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot
{
    public string cardArr; //각 슬롯에 배치된 카드들 조합 ex) 134 or jkq or 303 등등
    public Vector3 unitPos = new Vector3();
}

public class TestCardMatch : MonoBehaviour
{
    private CardSlot[] slots = new CardSlot[10]; //카드 배치하는 필드 배열
    private Dictionary<string, string> matches = new Dictionary<string, string>()
    {
        ["123"] = "unit1",
        ["jkq"] = "unit2", //조합이랑 유닛들 딕셔너리
    };

    private void GaemStart()
    {
        foreach(CardSlot s in slots)
        {
            if(!(s.cardArr.Length > 0)) continue;

            PoolableMono p = PoolManager.Instance.Pop(matches[s.cardArr]);
            p.transform.position = s.unitPos;
        }
    }
}
