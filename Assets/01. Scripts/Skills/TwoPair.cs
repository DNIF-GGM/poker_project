using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPair : UnitBase
{
    [Header("%제외 입력")]
    [SerializeField]
    float debuffValue;
    public override void SkillAttack()
    {
        base.SkillAttack();
        SetTarget(out Transform target,LayerMask.NameToLayer("Enemy"));
        target.GetComponent<IDamageable>().DownAtk(debuffValue/100);
    }
}
