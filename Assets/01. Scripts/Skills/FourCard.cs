using System.Collections;
using UnityEngine;

public class FourCard : UnitBase
{
    [SerializeField] float stunDuration = 3f;

    public override void SkillAttack()
    {
        base.SkillAttack();

        SetTarget(out Transform targetTrm, enemy, false);
        IStateable target = targetTrm.GetComponent<IStateable>();

        if(target.GetState().HasFlag(AgentState.Stun)) return;
    
        target.AddState(AgentState.Stun);

        StartCoroutine(StunCoroutine(stunDuration, target));
    }

    private IEnumerator StunCoroutine(float duration, IStateable target)
    {
        yield return new WaitForSeconds(duration);
        target.RemoveState(AgentState.Stun);
    }
}
