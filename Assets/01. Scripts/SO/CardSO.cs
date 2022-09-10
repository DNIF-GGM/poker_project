using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "SO/CardInfo")]
public class CardSO : ScriptableObject
{
    public CardEnum cardEnum;
    public Material mat;
    public float cost;
}
