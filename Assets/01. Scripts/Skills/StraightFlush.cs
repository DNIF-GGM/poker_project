using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class  StraightFlush : UnitBase
{
    [SerializeField] private AnimationClip _attackClip;

    protected override void Awake() {
        //_attackController.animationClips[0] = _attackClip;

        base.Awake();
    }

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
                if(col.GetComponent<UnitBase>() == null && !col.transform.CompareTag("Enemy")) continue;

                col.GetComponent<UnitBase>().Hit(10);
            }
        }

        base.SkillAttack();
    }
}
