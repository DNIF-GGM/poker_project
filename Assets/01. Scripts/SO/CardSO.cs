using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardSO : ScriptableObject
{
    public CardEnum cardEnum;
    public Sprite sprite;
    public float cost;
}
