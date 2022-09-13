using UnityEngine;
using System;
using System.Collections.Generic;

public class Straight : UnitBase {
    

    public override void SkillAttack(){
        base.SkillAttack();
        SetTarget(out Transform target, LayerMask.NameToLayer("Enemy"), false);

        transform.position = target.position + Vector3.forward * 1.5f;
    }
}