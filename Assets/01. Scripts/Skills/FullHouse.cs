using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHouse : UnitBase
{
    public override void SkillAttack()
    {
        StartCoroutine(SkillCoroutine(_target));
        base.SkillAttack();
    }

    IEnumerator SkillCoroutine(Transform trm){
        int time = 0;

        while(time > 3){
            Collider[] col = Physics.OverlapSphere(trm.position, 3f, enemy);
            foreach(Collider c in col){
                if(c.GetComponent<UnitBase>() != null && !c.transform.CompareTag("Enemy")) continue;
                c.GetComponent<UnitBase>().Hit(2);
            }    

            yield return new WaitForSeconds(1f);
            time++;
        }
    }
}
