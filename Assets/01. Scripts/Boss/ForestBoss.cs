using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : BossBase
{
    [SerializeField] private Transform _handTrm;

    public void BasicAttack(){
        if(target != null){
            Debug.Log("보스 공격");
            //IsAttack 트리거 켜줘야함
            target.transform.GetComponent<IDamageable>().OnDamage(Data._power);
        }
    }

    public void Skill1(){
        if(target != null){
            Debug.Log("돌 던지기");
            //IsSkill0 트리거 켜줘야함
            ThrowStone stone = PoolManager.Instance.Pop("Stone") as ThrowStone;
            stone.transform.position = _handTrm.position;
            stone.Throw(target);
        }
    }

    public void SpecialSkill(){
        //IsSkill1 트리거 켜줘야함
        UnitBase[] unitOnTables = GameObject.Find("GameManager/Pool").GetComponentsInChildren<UnitBase>();

        if(unitOnTables != null){
            foreach(UnitBase unit in unitOnTables){
                IDamageable iDamage = unit.GetComponent<IDamageable>();
                if(iDamage != null){
                    iDamage.OnDamage(3);
                }
            }
        }
    }

    public void Skill2(){
        //IsSkill1 트리거 켜줘야함
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4f, unitLayer);

        foreach(Collider col in colliders){
            if(col.transform.GetComponent<IDamageable>() != null || !col.transform.CompareTag("Unit")) continue;

            col.transform.GetComponent<IDamageable>().OnDamage(2);
            curHp += 5f;
        }
    }
}
