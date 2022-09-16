using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonsterBase
{
    public override void BasicAttack()
    {
        base.BasicAttack();
        Debug.Log(gameObject.name + " : " + _UnitHp);
        //이펙트 넣어야댐!
    }
}
