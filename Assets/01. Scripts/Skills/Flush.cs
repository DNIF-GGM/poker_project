using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flush : UnitBase
{
    public override void SkillAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(_target.position, 2.5f, enemy);

        foreach(Collider c in colliders){
            if(c.GetComponent<UnitBase>() != null && !c.transform.CompareTag("Enemy")) continue;

            c.GetComponent<UnitBase>().Hit(7);
        }


        base.SkillAttack();
    }
}
