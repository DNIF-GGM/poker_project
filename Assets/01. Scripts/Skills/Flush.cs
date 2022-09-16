using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flush : UnitBase
{
    public override void SkillAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(_target.position, 2.5f, enemy);

        foreach(Collider c in colliders){
            if(c.GetComponent<IDamageable>() != null || !c.transform.CompareTag("Enemy")) continue;
            c.GetComponent<IDamageable>().OnDamage(7);
        }
        Effect particle = PoolManager.Instance.Pop("Explosion") as Effect;
        particle.transform.SetParent(_target);
        particle.transform.position= _target.position;


        base.SkillAttack();
    }
}
