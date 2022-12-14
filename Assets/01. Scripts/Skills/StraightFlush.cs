using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class  StraightFlush : UnitBase
{
    protected override void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            SkillAttack();
        }

        base.Update();
    }

    public override void SkillAttack(){
        Collider[] colliders = GameObject.Find("GameManager/Pool").GetComponentsInChildren<Collider>();

        if(colliders.Length != 0){
            foreach(Collider col in colliders){
                if(col.GetComponent<IDamageable>() == null || !col.transform.CompareTag("Enemy")) continue;

                col.GetComponent<IDamageable>().OnDamage(10);
            }
        }

        base.SkillAttack();
    }
}
