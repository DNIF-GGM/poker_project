using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeEnum{
    spade,
    heart,
    dia,
    club
}

public enum NumberEnum{
    one = 0,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    ten,
    jack,
    queen,
    king
}

[System.Serializable]
public class CardEnum
{
    public ShapeEnum shapeEnum;
    public NumberEnum numberEnum;
}
