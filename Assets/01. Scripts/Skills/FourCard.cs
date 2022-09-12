using System.Collections;
using UnityEngine;

public class FourCard : UnitBase
{
    [SerializeField] float stunDuration = 3f;

    public override void SkillAttack()
    {
        base.SkillAttack();

        SetTarget(out Transform targetTrm, enemy, false);
        UnitBase targetUnit = targetTrm.GetComponent<UnitBase>();

        if(targetUnit._CurState.HasFlag(AgentState.Stun)) return;
    
        targetUnit._CurState |= AgentState.Stun;

        StartCoroutine(StunCoroutine(stunDuration, targetUnit));
    }

    private IEnumerator StunCoroutine(float duration, UnitBase target)
    {
        yield return new WaitForSeconds(duration);
        target._CurState &= ~AgentState.Stun;
    }
}
