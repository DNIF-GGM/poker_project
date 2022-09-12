using System.Collections;
using UnityEngine;

public class FourCard : UnitBase
{
    public override void SkillAttack()
    {
        base.SkillAttack();

        SetTarget(out Transform targetTrm, false);
        UnitBase targetUnit = targetTrm.GetComponent<UnitBase>();
    
        targetUnit._curState |= AgentState.Stun;

        StartCoroutine(StunCoroutine(1f, targetUnit));
    }

    private IEnumerator StunCoroutine(float duration, UnitBase target)
    {
        yield return new WaitForSeconds(duration);
        target._curState &= ~AgentState.Stun;
    }
}
