using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Agent/AgnetData")]
public class AgentDataSO : ScriptableObject {
    public AnimatorOverrideController controller; //애니메이션 컨드롤러
    public int _skillDamage;
    public int _hp; //체력
    public float _cycleTime; //사이클 주기 (평타 주기)
    public float _distance; //삭제예정
    public float _attackDistance; //공격 거리
    public float _speed; //속도
    public float _delay; //스킬 딜레이
    public float _power; //공격력
}
