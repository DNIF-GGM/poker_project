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

        Effect particle = PoolManager.Instance.Pop("Debuff")as Effect;
        particle.transform.SetParent(target);
        particle.transform.position = target.position;

        target.GetComponent<IDamageable>().DownAtk(debuffValue/100);
    }
}
