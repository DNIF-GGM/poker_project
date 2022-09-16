using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePair : UnitBase
{
    [SerializeField]
    float damage;
    public override void SkillAttack(){
        base.SkillAttack();
        SetTarget(out Transform target,LayerMask.NameToLayer("enemy"));
        Collider[] colliders = Physics.OverlapBox(transform.position,Vector3.forward*Vector3.Distance(target.position,transform.position),Quaternion.Euler(0,Mathf.Atan2(target.position.y-transform.position.y,target.position.x-transform.position.x)*Mathf.Rad2Deg,0));
        foreach(Collider c in colliders){
            c.GetComponent<IDamageable>().OnDamage(damage);
        }
        Debug.Log(gameObject.name + " " + _target.name + "때렸대요");
    }
}