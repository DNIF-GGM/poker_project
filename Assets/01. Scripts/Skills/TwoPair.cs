using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPair : UnitBase
{
    [SerializeField]
    public override void SkillAttack()
    {
        base.SkillAttack();
        SetTarget(out Transform target, enemy);

        Effect particle = PoolManager.Instance.Pop("Debuff") as Effect;
        particle.Init(3f);
        particle.transform.position = target.position; 
        particle.transform.SetParent(target);

        target.GetComponent<IDamageable>().DownAtk((float)(25 / 100));
    }
}
