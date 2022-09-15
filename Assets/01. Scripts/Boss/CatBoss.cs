using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CatBoss : BossBase
{
    public void SkillA()
    {
        target.GetComponent<IDamageable>().OnDamage(7);
        //찌릿찌릿 이펙트
    }

    public void SkillB()
    {
        //포효 이펙트
        UnitBase[] ubs = GameObject.Find("Pool").GetComponentsInChildren<UnitBase>();
        
        foreach(UnitBase ub in ubs)
        {
            if(!ub.CompareTag("Unit")) continue;

            ub.GetComponent<IDamageable>().OnDamage(1);
            StartCoroutine(StunCoroutine(1f, ub.GetComponent<IStateable>()));
        }
    }

    public void SkillC()
    {
        curHp = Mathf.Min(curHp + 3, GetMaxHp());
        //회복 이펙트

        UnitBase[] ubs = GameObject.Find("Pool").GetComponentsInChildren<UnitBase>();
        UnitBase targetUnit = target.GetComponent<UnitBase>();

        foreach(UnitBase ub in ubs)
            if(ub.CompareTag("Unit") && targetUnit._UnitHp < ub._UnitHp)
                targetUnit = ub;

        targetUnit.GetComponent<IDamageable>().OnDamage(7);
        //구체 발사 이펙트
    }

    public void SkillD()
    {
        //선딜레이 애니메이션
        AddState(AgentState.Stun);

        UnitBase[] ubs = GameObject.Find("Pool").GetComponentsInChildren<UnitBase>();
        UnitBase targetUnit = target.GetComponent<UnitBase>();

        foreach(UnitBase ub in ubs)
            if(ub.CompareTag("Unit") && targetUnit._UnitHp > ub._UnitHp)
                targetUnit = ub;

        StartCoroutine(SkillDCoroutine(3f, targetUnit.GetComponent<IDamageable>()));
    }

    private IEnumerator SkillDCoroutine(float firstDuration, IDamageable id)
    {
        yield return new WaitForSeconds(firstDuration);
        id.OnDamage(10000f);
        RemoveState(AgentState.Stun);
        //죽어랏 이펙트
    }

    private IEnumerator StunCoroutine(float duration, IStateable ist)
    {
        ist.AddState(AgentState.Stun);
        yield return new WaitForSeconds(duration);
        ist.RemoveState(AgentState.Stun);
    }
}
