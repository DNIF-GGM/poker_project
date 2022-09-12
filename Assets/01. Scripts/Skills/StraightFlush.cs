using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class  StraightFlush : UnitBase
{
    private bool _isAttack;

    protected override void Update() {
        if(Input.GetKeyDown(KeyCode.G)){
            SkillAttack();
        }

        base.Update();
    }

    public override void SkillAttack(){
        Collider[] colliders = GameObject.Find("Test").GetComponentsInChildren<Collider>();

        if(colliders.Length != 0){
            foreach(Collider col in colliders){
                if(col.GetComponent<UnitBase>() == null) continue;

                col.GetComponent<UnitBase>().Hit(10);
            }
        }

        base.SkillAttack();
    }
}
