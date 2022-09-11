using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Agent/AgnetData")]
public class AgnetDataSO : ScriptableObject {
    public int _hp;
    public float _cycleTime;
    public float _distance;
    public float _attackDistance;
    public float _speed;
    public float _delay;
    public float _power;
}