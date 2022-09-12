using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flush : UnitBase
{
    [SerializeField] private AnimationClip _attackClip;

    protected override void Awake() {
        //_attackController.animationClips[0] = _attackClip;

        base.Awake();
    }

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
